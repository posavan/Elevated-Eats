using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {

        IRecipeDao dao;
        public RecipeController(IRecipeDao RecipeDao)
        {
            this.dao = RecipeDao;
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




        [HttpGet("{userId}/{recipeId}/ingredients")]
        public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
        {
            return Ok(dao.GetIngredientsByRecipeId(recipeId));
        }


        [HttpPost()]
        public ActionResult<Recipe> CreateRecipe(Recipe newRecipe)
        {
            Recipe result = dao.CreateRecipe(newRecipe);

            if (result.RecipeId == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("{userId}/{recipeId}")]
        public ActionResult<Recipe> AddRecipeToUser(int userId, int recipeId)
        {
            int result = dao.AddRecipeToUser(userId, recipeId);

            if (result < 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
