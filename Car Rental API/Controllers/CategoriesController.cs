using BusinessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.FuelTyp;
using DataAccessLayer.Models.Vehicle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Rental_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<CreateFuelTypeDTO>>> GetCategories()
        {
            var Categories = await Category.GetAllCategories();

            if (Categories.Count() == 0)
            {
                return NotFound("No Categories found.");
            }

            return Ok(Categories);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var Category = await BusinessLayer.Category.Find(id);

            if (Category == null)
                return NotFound($"Category With Id {id} Not Found");

            return Ok(Category.CategoryDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CategoryDTO>> AddNewCategory(CreateCategoryDTO dto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage);
                return BadRequest(errors);
            }

            var Category = new Category(dto.Name);

            if (!await Category.Save())
            {
                return BadRequest("Failed in add new Category.");
            }

            return CreatedAtRoute("GetCategory", new { id = Category.ID }, Category.CategoryDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, CreateCategoryDTO dto)
        {
            if (id <= 0 || !ModelState.IsValid)
                return BadRequest("Invalid");

            var category = await Category.Find(id);

            if (category == null)
                return NotFound($"category with id {id} not found!");

            category.Name = dto.Name;


            if (!await category.Save())
                return BadRequest("Failed!");

            return Ok(category.CategoryDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");

            var category = await Category.Find(id);

            if (category == null)
                return NotFound($"category with id {id} not found!");


            if (await category.Delete())
                return Ok("Deleted Successfully.");
            else
                return BadRequest($"Failed delete category with id {id}");
        }








    }
}
