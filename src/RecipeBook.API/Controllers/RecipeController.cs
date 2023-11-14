using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RecipeBook.ServiceLibrary.Domains;
using RecipeBook.ServiceLibrary.Entities;
using System;
using System.Threading.Tasks;

namespace RecipeBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        /*
        [HttpGet]
        public IActionResult AddNewRecipe([FromQuery]RecipeEntity recipeEntity) 
        {
            var businessLogic = new Recipe();
            businessLogic.SaveRecipe(recipeEntity);
            return Ok();
        }
        */

        [HttpGet]
        public async Task<IActionResult> GetListAsync(
            [FromQuery]int pageSize, 
            [FromQuery]int pageNumber)
        {
            return Ok(pageSize + " " + pageNumber);
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetOnceAsync(
            [FromRoute]Guid recipeId)
        {
            return Ok(recipeId);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRecipe(
            [FromBody] RecipeEntity recipe)
        {
            return Ok(recipe);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRecipeAsync(
            [FromBody] RecipeEntity recipe)
        {
            return Ok(recipe);
        }

        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> deleteRecipeAsync(Guid recipeId)
        {
            return Ok(recipeId);
        }


    }

    
}
