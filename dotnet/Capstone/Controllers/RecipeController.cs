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
        IUserDao userDao;
        public RecipeController(IRecipeDao RecipeDao, IUserDao UserDao)
        {
            this.dao = RecipeDao;
            this.userDao = UserDao;
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
        public ActionResult<Recipe> AddRecipeToUser(int recipeId)
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
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
