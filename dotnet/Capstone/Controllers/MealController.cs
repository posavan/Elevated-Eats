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

        // /meal
        [HttpGet()]
        public ActionResult<List<Meal>> ListMeals()
        {
            return Ok(dao.ListMeals());
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


        // meal/1
        [HttpPut("{mealId}")]
        public ActionResult<Meal> UpdateMeal(int mealId, Meal updatedMeal)
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

        [HttpDelete("{mealId}")]
        public ActionResult<Meal> DeleteMeal(int mealId)
        {

            if (dao.DeleteMeal(mealId) == true)
            {
                return NotFound();
            }
            if (dao.DeleteMeal(mealId))
            {
                return NoContent();
            }

            return NotFound();
        }

    }
}
