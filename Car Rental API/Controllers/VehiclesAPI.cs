using DataAccessLayer.Models.Vehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer;

namespace Car_Rental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IEnumerable<VehicleReadDTO>>> GetVehicles()
        {
            var vehicles = await Vehicle.GetAllVehicles();

            if(vehicles.Count == 0)
            {
                return NotFound("No vehicles found.");
            }

            return Ok(vehicles);
        }
        
        [HttpGet("{id}",Name = "GetVehicleByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<VehicleReadDTO>> GetVehicleByID(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid ID!");

            var vehicle =await Vehicle.Find(id);

            if(vehicle == null)
            {
                return NotFound("No Vehicle Founded!");
            }

            return Ok(await vehicle.ToReadDTO());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<VehicleUpdateDTO>> AddNewVehicle(VehicleCreateDTO VDTO)
        {
            var vehicle = new Vehicle(VDTO);
            
            if(!await vehicle.Save())
            {
                return BadRequest("Failed in add new vehicle.");
            }

            return CreatedAtRoute("GetVehicleByID", new { id = vehicle.VehicleID },await vehicle.ToReadDTO());
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VehicleReadDTO>> UpdateVehicle(int id ,VehicleUpdateDTO VDTO)
        {
            if(VDTO == null || id <= 0 || VDTO.FuelTypeID<= 0 ||VDTO.VehicleCategoryID <= 0)
                return BadRequest("Invalid");

            var vehicle = await Vehicle.Find(id);

            if (vehicle == null)
                return NotFound($"Vehicle with id {id} not found!");

            VDTO.Id = id;
            vehicle = new Vehicle(VDTO);
            

            if (!await vehicle.Save())
                return BadRequest("Failed!");

            return Ok(await vehicle.ToReadDTO());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            if ( id <= 0 )
                return BadRequest("Invalid Id");

            var vehicle = await Vehicle.Find(id);

            if (vehicle == null)
                return NotFound($"Vehicle with id {id} not found!");


            if(await vehicle.DeleteVehicle())
                return Ok("Deleted Successfully.");
            else
                return BadRequest($"Failed delete vehicle with id {id}");
        }



    }
}
