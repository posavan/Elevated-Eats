using Capstone.Models;
using System.Collections;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IIngredientDao
    {
        public IList<Ingredient> GetIngredients();
        public Ingredient GetIngredientById(int id);
        public Ingredient GetIngredientByName(string name);
        public Ingredient CreateIngredient(Ingredient ingredient);

    }
}
