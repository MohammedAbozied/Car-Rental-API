using BusinessLayer;
using DataAccessLayer.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<UserInfoDTO>> RegisterNewUser(NewUserDTO User_DTO)
        {
            var user = new User(User_DTO);

            if (!await user.Save())
            {
                return BadRequest("Failed.");
            }

            return CreatedAtRoute("GetUserByID", new { id = user.UserId }, user.UserInfoDTO);
        }












    }

}
