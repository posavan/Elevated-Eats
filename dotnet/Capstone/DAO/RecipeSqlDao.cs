using Capstone.Exceptions;
using Capstone.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Capstone.DAO
{
    public class RecipeSqlDao : IRecipeDao
    {

        private readonly string connectionString;

        public RecipeSqlDao(string dbConnectionString)
        {

            this.connectionString = dbConnectionString;
        }

        public IList<Recipe> GetRecipes()
        {
            IList<Recipe> recipes = new List<Recipe>();

            string sql = "SELECT recipe_id, recipe_name, recipe_description FROM recipes;";


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Recipe recipe = MapRowToRecipe(reader);
                        recipe.IngredientList = GetIngredientsByRecipeId(recipe.RecipeId);
                        recipes.Add(recipe);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipes;
        }

        public Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = null;

            string sql = "SELECT recipe_id, recipe_name, recipe_description FROM recipes WHERE recipe_id = @recipe_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        recipe = MapRowToRecipe(reader);
                        recipe.IngredientList = GetIngredientsByRecipeId(recipe.RecipeId);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipe;
        }

        public Recipe GetRecipeByName(string name)
        {
            Recipe recipe = null;

            string sql = "SELECT recipe_id, recipe_name, recipe_description FROM recipes WHERE recipe_name = @name";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        recipe = MapRowToRecipe(reader);
                        recipe.IngredientList = GetIngredientsByRecipeId(recipe.RecipeId);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return recipe;
        }

        public List<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            string sql = "SELECT i.ingredient_id, ingredient_name, calories FROM ingredients i " +
                "JOIN recipes_ingredients ri ON i.ingredient_id = ri.ingredient_id " +
                "WHERE ri.recipe_id = @recipe_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Ingredient ingredient = MapRowToIngredient(reader);
                        ingredients.Add(ingredient);

                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return ingredients;

        }


        public Recipe CreateRecipe(Recipe recipe)
        {
            Recipe newRecipe = null;

            string sql = "INSERT INTO recipes (recipe_name, recipe_description) " +
                         "OUTPUT INSERTED.recipe_id " +
                         "VALUES (@name, @description);";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@description", recipe.RecipeDescription);
                    recipe.RecipeId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                AddIngredientsToRecipe(recipe);
                newRecipe = GetRecipeById(recipe.RecipeId);

            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newRecipe;
        }

        public void AddIngredientsToRecipe(Recipe recipe)
        {

            string sql = "INSERT INTO recipes_ingredients (recipe_id, ingredient_id) " +
                         "VALUES(@recipe_id, @ingredient_id);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int i = 0;
                    while (i < recipe.IngredientList.Count)
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);
                        cmd.Parameters.AddWithValue("@ingredient_id", recipe.IngredientList[i]);
                        Convert.ToInt32(cmd.ExecuteScalar());
                        i++;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return;

        }

        private Recipe MapRowToRecipe(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            recipe.RecipeName = Convert.ToString(reader["recipe_name"]);
            recipe.RecipeDescription = Convert.ToString(reader["recipe_description"]);
            return recipe;
        }

        private Ingredient MapRowToIngredient(SqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.IngredientId = Convert.ToInt32(reader["ingredient_id"]);
            ingredient.IngredientName = Convert.ToString(reader["ingredient_name"]);
            ingredient.Calories = Convert.ToInt32(reader["calories"]);
            return ingredient;
        }
    }
}
