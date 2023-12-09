using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {

        IRecipeDao dao;
        IUserDao userDao;
        IIngredientDao ingredientDao;
        public RecipeController(IRecipeDao RecipeDao, IUserDao UserDao, IIngredientDao IngredientDao)
        {
            this.dao = RecipeDao;
            this.userDao = UserDao;
            this.ingredientDao = IngredientDao;
        }


        [HttpGet()]
        public ActionResult<List<Recipe>> GetRecipes()
        {
            return Ok(dao.GetRecipes());
        }

        [HttpGet("favorites")]
        public ActionResult<List<Recipe>> GetRecipesByUserId()
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            return Ok(dao.GetRecipesByUserId(userId));
        }


        [HttpGet("favorites/{recipeId}")]
        public ActionResult<Recipe> GetUserRecipeById(int recipeId)
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            return Ok(dao.GetUserRecipeById(recipeId));
        }

        [HttpGet("public/{recipeName}")]
        public ActionResult<Recipe> GetRecipeByName(string recipeName)
        {
            return Ok(dao.GetRecipeByName(recipeName));
        }

        [HttpGet("favorites/{recipeId}/ingredients")]
        public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
        {
            return Ok(dao.GetIngredientsByRecipeId(recipeId));
        }


        [HttpPost()]
        public ActionResult<Recipe> CreateRecipe(Recipe newRecipe)
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            Recipe result = dao.CreateRecipe(newRecipe, userId);

            if (result == null || result.RecipeId == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("favorites")]
        public ActionResult<Recipe> AddRecipeToUser(Recipe newRecipe)
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            Recipe result = dao.AddRecipeToUser(newRecipe, userId);

            return Ok(result);
        }

        [HttpDelete("favorites/{recipeId}")]
        public ActionResult<Recipe> RemoveRecipeFromUser(int recipeId)
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            Recipe recipe = dao.GetUserRecipeById(recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            if (dao.RemoveRecipeFromUser(recipe.RecipeName, userId))
            {
                return NoContent();
            }

            return NotFound();

        }

        [HttpPut("favorites/edit")]
        public ActionResult<Recipe> ChangeRecipe(Recipe changedRecipe)
        {
            Recipe newRecipe = dao.ModifyRecipe(changedRecipe);

            if (newRecipe == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newRecipe);
            }

        }

        [HttpPut("favorites/edit/ingredients")]
        public ActionResult<Recipe> AddIngredientsToRecipe(Recipe recipe)
        {
            // Check if each ingredient exists in the master list
            foreach (Ingredient ingredient in recipe.IngredientList)
            {
                if (ingredientDao.IngredientExists(ingredient))
                {
                    // Handle the case where the ingredient already exist
                    return BadRequest($"Ingredient {ingredient.IngredientName} already exists in the master list.");
                }
            }

            // Call the method to add ingredients to the recipe
            dao.AddIngredientsToRecipe(recipe);

            //return a success response
            return Ok("Ingredients added successfully to the recipe.");

        }

        [HttpDelete("favorites/{recipeId}/ingredients/{ingredientId}")]
        public ActionResult RemoveIngredientFromRecipe(int recipeId, int ingredientId)
        {
            try
            {
                dao.RemoveIngredientFromRecipe(recipeId, ingredientId);
                return NoContent();

            }
            catch (DaoException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
