using BusinessLayer;
using DataAccessLayer.Models.User;
using DataAccessLayer.Models.Vehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Car_Rental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("{id}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<UserInfoDTO>> GetUserByID(int id) 
        {
            if (id <= 0)
                return BadRequest("Invalid ID!");

            var user = await BusinessLayer.User.Find(id);

            if (user == null)
            {
                return NotFound("Not Found!");
            }

            return Ok(user.UserInfoDTO);
        }

        [HttpPost("reg")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<UserInfoDTO>> RegisterNewUser(NewUserDTO User_DTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var user = new User(User_DTO);

            if (!await user.Save())
            {
                return BadRequest("Failed.\nmay be email is already existed.");
            }

            return CreatedAtRoute("GetUserByID", new { id = user.UserId }, user.UserInfoDTO);
        }
        
        [HttpPost("auth/login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginDTO User_DTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var user =await BusinessLayer.User.Find(User_DTO.Email);

            if(user == null)
            {
                return NotFound($"No User With This Email: {User_DTO.Email}.");
            }

            if(!await user.CheckPassword(User_DTO.Password))
            {
                return Unauthorized("Incorrect password.");
            }
            
            return Ok(new LoginResponseDTO("Token, SOON!",user.UserInfoDTO));
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VehicleReadDTO>>> GetAllUsers()
        { 
            var users = await BusinessLayer.User.GetAllUsers();

            if (users.Count == 0)
            { 
                return NotFound("No Users found.");
            }

            return Ok(users);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserInfoDTO>> UpdateUser(int id,UpdateUserDTO User_DTO )
        {
            if (id <= 0 ||User_DTO is null || !ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var user = await BusinessLayer.User.Find(id);

            if (user == null)
                return NotFound($"User with id {id} not found!");

            user.CopyFrom(User_DTO);

            if (!await user.Save())
                return BadRequest("Failed!");

            return Ok(user.UserInfoDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var user = await BusinessLayer.User.Find(id);

            if (user == null)
                return NotFound($"User with id {id} not found!");


            if (await user.DeleteUser())
                return Ok("Deleted Successfully.");
            else
                return BadRequest($"Failed.");
        }






    }

}
