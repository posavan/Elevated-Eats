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
    public class MealController : ControllerBase
    {
        IMealDao dao;

        public MealController(IMealDao MealDao)
        {
            this.dao = MealDao;
        }

        [HttpGet()]
        public ActionResult<List<Meal>> ListMeals()
        {
            return Ok(dao.ListMeals());
        }

        [HttpGet("{mealId}")]
        public ActionResult<Meal> GetMeal(int mealId)
        {
            return Ok(dao.GetMeal(mealId));
        }
    }
}
