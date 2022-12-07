
﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Cheaper_Effort.Serivces;
using Cheaper_Effort.Models;
using Cheaper_Effort.Pages;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public IEnumerable<RecipeWithIngredients> GetAllRecipes()
    {
        return _recipeService.GetRecipes();
    }
    [HttpGet("Delete/{id}")]
    public ActionResult GetByIdRecipes([FromBody] Guid id)
    {
        return RedirectToPage("/RecipePages/Delete", new { RecipeWithIngredients = _recipeService.GetRecipeById(id) });
    }


    [HttpPost]
    /*public async Task<IActionResult> AddRecipe([FromBody] RecipeWithId recipeWithId)
    {
        if (recipeWithId == null)
            return BadRequest();

        else
            await _newRecipeService.addRecipeToDBAsync(recipeWithId);
        return Created("Created", true);
    }*/

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] Guid id)
    {
        await _recipeService.Delete(id);
        return RedirectToPage("/RecipePages/Recipes");
    }

}
