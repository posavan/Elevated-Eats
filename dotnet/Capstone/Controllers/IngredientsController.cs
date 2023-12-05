using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        IIngredientDao dao;
        public IngredientsController(IIngredientDao ingredientDao)
        {
            this.dao = ingredientDao;
        }


        [HttpGet()]
        public ActionResult<List<Ingredient>> GetIngredients()
        {
            return Ok(dao.GetIngredients());
        }

        

        [HttpPost()]
        public ActionResult<Ingredient> CreateIngredient(Ingredient newIngredient)
        {
            Ingredient result = dao.CreateIngredient(newIngredient);

            if (result.IngredientId == 0)
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
