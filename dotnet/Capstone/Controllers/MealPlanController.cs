using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MealPlanController : ControllerBase
    {
        IMealPlanDao dao;
        IUserDao userDao;
        public MealPlanController(IMealPlanDao MealPlanDao, IUserDao UserDao)
        {
            this.dao = MealPlanDao;
            this.userDao = UserDao;
        }

        [HttpGet()]
        public ActionResult<List<MealPlan>> ListMealPlans()
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            return Ok(dao.ListMealPlansByUserId(userId));
        }

        [HttpGet("{mealPlanId}")]
        public ActionResult<MealPlan> GetMealPlanById(int mealPlanId)
        {
            return Ok(dao.GetMealPlanById(mealPlanId));
        }

        [HttpPost()]
        public ActionResult<MealPlan> CreateMealPlan(MealPlan newMealPlan)
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            MealPlan result = dao.CreateMealPlan(newMealPlan, userId);
            if (result == null || result.MealPlanId == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPut()]
        public ActionResult<MealPlan> UpdateMealPlan(MealPlan mealPlan)
        {
            int userId = userDao.GetUserByUsername(User.Identity.Name).UserId;
            MealPlan result = dao.UpdateMealPlan(mealPlan, userId);
            if (result == null || result.MealPlanId == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost("{mealPlanId}")]
        public ActionResult<MealPlan> AddMealToMealPlan(int mealPlanId, int mealId)
        {
            bool result = dao.AddMealToMealPlan(mealPlanId, mealId);

            return Ok(result);
        }

        [HttpDelete("{mealPlanId}/{mealId}")]
        public ActionResult<MealPlan> RemoveMealFromMealPlan(int mealPlanId, int mealId)
        {
            bool result = dao.RemoveMealFromMealPlan(mealPlanId, mealId);

            return Ok(result);
        }

        [HttpDelete("{mealPlanId}")]
        public ActionResult<MealPlan> DeleteMealPlan(int mealPlanId)
        {
            bool result = dao.DeleteMealPlan(mealPlanId);

            return Ok(result);
        }

    }
}
