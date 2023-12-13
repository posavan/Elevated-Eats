using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MealController : ControllerBase
    {
        IMealDao dao;

        public MealController(IMealDao MealDao)
        {
            this.dao = MealDao;
        }

        // meal
        [HttpGet()]
        public ActionResult<List<Meal>> ListMeals()
        {
            return Ok(dao.ListMeals());
        }

        // meal/mealId/recipes

        [HttpGet("{mealId}/recipes")]
        public ActionResult<List<Recipe>> GetRecipesByMealId(int mealId)
        {
            return Ok(dao.GetRecipesByMealId(mealId));
        }

        // meal/1

        [HttpGet("{mealId}")]
        public ActionResult<Meal> GetMeal(int mealId)
        {
            return Ok(dao.GetMeal(mealId));
        }


        // meal/
        [HttpPost()]
        public ActionResult<Meal> CreateMeal(Meal newMeal)
        {
            Meal result = dao.CreateMeal(newMeal);

            if (result.MealId == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }


        // meal
        [HttpPut()]
        public ActionResult<Meal> UpdateMeal(Meal updatedMeal)
        {
            Meal newMeal = dao.UpdateMeal(updatedMeal);

            if (newMeal == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(newMeal);
            }
        }

        [HttpPost("{mealId}")]
        public ActionResult<Meal> AddRecipeToMeal(int mealId, Recipe recipe)
        {
            bool result = dao.AddRecipeToMeal(mealId, recipe);

            return Ok(result);
        }

        [HttpDelete("{mealId}/{recipeId}/")]
        public ActionResult<Meal> RemoveRecipeFromMeal(int mealId, int recipeId)
        {
            bool result = dao.RemoveRecipeFromMeal(mealId, recipeId);

            return Ok(result);
        }

        [HttpDelete("{mealId}")]
        public ActionResult<Meal> DeleteMeal(int mealId)
        {

            bool result = dao.DeleteMeal(mealId);

            return Ok(result);
        }

    }
}
