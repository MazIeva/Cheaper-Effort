
﻿using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cheaper_Effort.Serivces;
using Cheaper_Effort.Models;
using Cheaper_Effort.Pages;
using Cheaper_Effort.Data;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipeController : ControllerBase
{
    private IRecipeService _recipeService;
    private INewRecipeService _newRecipeService;

    public RecipeController(IRecipeService recipeService, INewRecipeService newRecipeService)
    {
        _recipeService = recipeService;
        _newRecipeService = newRecipeService;
    }
    
    [Route("DeleteRecipe")]
    [HttpDelete]
    public IActionResult DeleteRecipe([FromForm] Guid  id)
    {
       
            _recipeService.Delete(id);
           return Ok();
        
    }

}
