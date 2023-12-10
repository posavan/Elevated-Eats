using Capstone.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MealPlanController : ControllerBase
    {
        IMealPlanDao dao;
        IMealDao mealDao;
        IRecipeDao recipeDao;
        IUserDao userDao;
        IIngredientDao ingredientDao;
        public MealPlanController(IMealPlanDao MealPlanDao,IRecipeDao RecipeDao, IUserDao UserDao, IIngredientDao IngredientDao, IMealDao MealDao)
        {
            this.dao = MealPlanDao;
            this.mealDao = MealDao;
            this.recipeDao = RecipeDao;
            this.userDao = UserDao;
            this.ingredientDao = IngredientDao;
        }







    }
}
