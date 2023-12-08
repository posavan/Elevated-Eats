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
        public RecipeController(IRecipeDao RecipeDao, IUserDao UserDao, IIngredientDao IngredientDao )
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

        [HttpGet("{userId}")]
        public ActionResult<List<Recipe>> GetRecipesByUserId(int userId)
        {
            return Ok(dao.GetRecipesByUserId(userId));
        }


        [HttpGet("{userId}/{recipeId}")]
        public ActionResult<Recipe> GetUserRecipeById(int recipeId)
        {
            return Ok(dao.GetUserRecipeById(recipeId));
        }

        [HttpGet("public/{recipeName}")]
        public ActionResult<Recipe> GetRecipeByName(string recipeName)
        {
            return Ok(dao.GetRecipeByName(recipeName));
        }


        [HttpGet("{userId}/{recipeId}/ingredients")]
        public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
        {
            return Ok(dao.GetIngredientsByRecipeId(recipeId));
        }


        [HttpPost()]
        public ActionResult<Recipe> CreateRecipe(Recipe newRecipe)
        {
            int currentUser = userDao.GetUserByUsername(User.Identity.Name).UserId;
            Recipe result = dao.CreateRecipe(newRecipe, currentUser);

            if (result == null || result.RecipeId == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("{userId}")]
        public ActionResult<Recipe> AddRecipeToUser(Recipe newRecipe)
        {

            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            Recipe result = dao.AddRecipeToUser(newRecipe, userId);

            return Ok(result);
        }

        [HttpDelete("{userId}/{recipeId}")]
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

        [HttpPost("{userId}/{recipeId}")]
        public ActionResult<Recipe> AddIngredientsToRecipe(int userId, int recipeId, List<Ingredient> ingredientList)
        {

            // Check if each ingredient exists in the master list
            foreach (Ingredient ingredient in ingredientList)
            {
                if (!ingredientDao.IngredientExists(ingredient))
                {
                    // Handle the case where the ingredient doesn't exist
                    return BadRequest($"Ingredient {ingredient.IngredientName} does not exist in the master list.");
                }
            }

            // Call the method to add ingredients to the recipe
            dao.AddIngredientsToRecipe(userId, recipeId, ingredientList);

            // Optionally, return a success response
            return Ok("Ingredients added successfully to the recipe.");


        }


        [HttpPut("{userId}/{recipeName}")]
        public ActionResult<Recipe> ChangeRecipe(int userRecipeId, Recipe changedRecipe)
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

        [HttpDelete("{userId}/{recipeId}/ingredients/{ingredientId}")]
        public ActionResult RemoveIngredientsFromRecipe(int userRecipeId, int ingredientId)
        {
            try
            {
                dao.RemoveIngredientsFromRecipe(userRecipeId, ingredientId);
                return NoContent();

            }
            catch (DaoException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
