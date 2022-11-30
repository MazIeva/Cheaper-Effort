using System;
using Microsoft.AspNetCore.Mvc;
using Cheaper_Effort.Serivces;
using Cheaper_Effort.Models;
using Cheaper_Effort.Pages;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private RecipeService _recipeService;
    private NewRecipeService _newRecipeService;

    public RecipeController(RecipeService recipeService, NewRecipeService newRecipeService)
    {
        _recipeService = recipeService;
        _newRecipeService = newRecipeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeWithIngredients>>> GetAllRecipes()
    {
        var list =  _recipeService.GetRecipes();

        if (!list.Any())
        {
            return NoContent();
        }

        return Ok(list);
    }


   /* [HttpPost]
    public async Task<IActionResult> AddRecipe([FromBody] Recipe recipe, string[] ingredientIds)
    {
        if (recipe == null)
            return BadRequest();

        else
            await _newRecipeService.addRecipeToDBAsync(recipe, ingredientIds);
        return Created("Created", true);
    }*/

    [HttpDelete]
    public async Task<IActionResult> DeleteRecipe(string id)
    {
        await _recipeService.Delete(id);
        return NoContent();
    }

    [HttpPost]
    [Route("match")]
    public async Task<ActionResult<IEnumerable<RecipeWithIngredients>>> RecipeByIngredients([FromBody] string[] ingredientIds)
    {
        if (ingredientIds.Any())
        {
            return Ok( _recipeService.SearchRecipe(ingredientIds, _recipeService.GetRecipes()));
        }
        return BadRequest();
    }

}


