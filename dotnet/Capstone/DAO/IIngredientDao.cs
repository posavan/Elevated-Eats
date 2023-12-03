using Capstone.Models;
using System.Collections;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IIngredientDao
    {
        IList<Ingredient> GetIngredients();

        Ingredient GetIngredientById(int id);

        Ingredient GetIngredientByName(string name);
        Ingredient CreateIngredient(Ingredient ingredient);


    }
}
