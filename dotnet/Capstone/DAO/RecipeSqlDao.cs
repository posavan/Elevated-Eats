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
        private readonly IngredientSqlDao ingredientDao;

        public RecipeSqlDao(string dbConnectionString)
        {

            this.connectionString = dbConnectionString;
            this.ingredientDao = new IngredientSqlDao(dbConnectionString);
        }

        public IList<Recipe> GetRecipes()
        {
            IList<Recipe> recipes = new List<Recipe>();

            string sql = "SELECT recipe_id, recipe_name, recipe_instructions FROM recipes;";


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

        public IList<Recipe> GetRecipesByUserId(int userId)
        {
            IList<Recipe> recipes = new List<Recipe>();

            string sql = "SELECT user_recipe_id, recipe_name, recipe_instructions FROM users_saved_recipes " +
                "WHERE user_id = @user_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Recipe recipe = MapRowToUserRecipe(reader);
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

        public Recipe GetUserRecipeById(int recipeId)
        {
            Recipe recipe = null;

            string sql = "SELECT user_recipe_id, recipe_name, recipe_instructions FROM users_saved_recipes " +
                "WHERE user_recipe_id = @recipe_id";

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
                        recipe = MapRowToUserRecipe(reader);
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

        // retrieves from users saved recipes
        

        //get rid of this method -- duplicate
        public Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = null;

            string sql = "SELECT user_recipe_id, recipe_name, recipe_instructions FROM users_saved_recipes " +
                "WHERE user_recipe_id = @recipe_id";

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
                        recipe = MapRowToUserRecipe(reader);
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

        public Recipe GetRecipeByName(string recipeName)
        {
            Recipe recipe = null;

            string sql = "SELECT recipe_id, recipe_name, recipe_instructions FROM recipes " +
                "WHERE recipe_name = @name";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", recipeName);
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

            string sql = "SELECT i.ingredient_id, ingredient_name FROM ingredients i " +
                "JOIN recipes_ingredients ri ON i.ingredient_id = ri.ingredient_id " +
                "WHERE ri.user_recipe_id = @recipe_id";

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

        public Recipe AddRecipeToUser(Recipe recipe, int userId)
        {
            Recipe newRecipe = null;
            string sql = "INSERT INTO users_saved_recipes (user_id, recipe_name, recipe_instructions) " +
                         "VALUES (@user_id, @recipe_name, @recipe_instructions);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@recipe_instructions", recipe.RecipeInstructions);
                    cmd.ExecuteScalar();
                    //newRecipe = GetRecipeById(Convert.ToInt32(cmd.ExecuteScalar())); returns null
                }

            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newRecipe;
        }

        public bool RemoveRecipeFromUser(string recipeName, int userId)
        {
            bool result = false;
            string priorSql = "DELETE FROM recipes_ingredients WHERE user_recipe_id IN " +
                "(SELECT ri.user_recipe_id FROM recipes_ingredients ri " +
                "JOIN users_saved_recipes usr ON ri.user_recipe_id=usr.user_recipe_id " +
                "WHERE usr.recipe_name=@recipe_name);";
            string sql = "DELETE FROM users_saved_recipes WHERE recipe_name=@recipe_name AND user_id=@user_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(priorSql, conn);
                    cmd.Parameters.AddWithValue("@recipe_name", recipeName);
                    int count = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@recipe_name", recipeName);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        result = true;
                    }
                }

            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return result;
        }

        // creates recipe in User's recipes
        public Recipe CreateRecipe(Recipe recipe, int userId)
        {
            Recipe newRecipe = null;

            string sql = "INSERT INTO users_saved_recipes (user_id, recipe_name, recipe_instructions) " +
                         "OUTPUT INSERTED.user_recipe_id " +
                         "VALUES (@user_id, @name, @instructions);";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@name", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@instructions", recipe.RecipeInstructions);
                    newRecipe = GetRecipeById(Convert.ToInt32(cmd.ExecuteScalar()));
                }

                newRecipe = GetRecipeById(recipe.RecipeId);


            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newRecipe;
        }

        public Recipe ModifyRecipe(Recipe updatedRecipe)
        {
            //method to update a recipe that a user has saved
            string sqlUpdateRecipe = "UPDATE users_saved_recipes SET recipe_name = @recipe_name, recipe_instructions = @recipe_instructions  " +
                            "WHERE user_recipe_id = @user_recipe_id";


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateRecipe, conn);
                    cmd.Parameters.AddWithValue("@recipe_name", updatedRecipe.RecipeName);
                    cmd.Parameters.AddWithValue("@recipe_instructions", updatedRecipe.RecipeInstructions);
                    cmd.Parameters.AddWithValue("@user_recipe_id", updatedRecipe.RecipeId);

                    int count = cmd.ExecuteNonQuery();
                    if (count == 1)
                    {
                        return updatedRecipe;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

        }

        public void AddIngredientsToRecipe(Recipe recipe)
        {
            string sqlAddNewIngredient = "INSERT INTO ingredients (ingredient_name) " +
                "OUTPUT INSERTED.ingredient_id " +
                "VALUES (@ingredientName);";

            string sqlAddIngredientToRecipe = "INSERT INTO recipes_ingredients (user_recipe_id, ingredient_id, quantity) " +
                         "VALUES(@recipe_id, @ingredient_id, @quantity);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    foreach (Ingredient ingredient in recipe.IngredientList)
                    {
                        // check if ingredient already exists in the master list (db does not allow duplicates)
                        //if (!ingredientDao.IngredientExists(ingredient))
                        //{
                            // add the new ingredient to the master list
                            SqlCommand cmdNewIngredient = new SqlCommand(sqlAddNewIngredient, conn);
                            cmdNewIngredient.Parameters.AddWithValue("@ingredientName", ingredient.IngredientName);

                            // assign new ingredient's id
                            int newIngredientId = Convert.ToInt32(cmdNewIngredient.ExecuteScalar());

                            if (newIngredientId > 0)
                            {
                                // add the ingredient to recipes_ingredients
                                SqlCommand cmdAddToRecipe = new SqlCommand(sqlAddIngredientToRecipe, conn);
                                cmdAddToRecipe.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);
                                cmdAddToRecipe.Parameters.AddWithValue("@ingredient_id", newIngredientId);
                                cmdAddToRecipe.Parameters.AddWithValue("@quantity", ingredient.Quantity);

                                int rowsAffected = cmdAddToRecipe.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine($"Ingredient {ingredient.IngredientName} added successfully to recipe.");
                                }
                                else
                                {
                                    Console.WriteLine($"Failed to add ingredient {ingredient.IngredientName} to the recipe.");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Failed to add ingredient {ingredient.IngredientName} to the master list.");
                            }
                        //}
                        //else
                        //{
                        //    Console.WriteLine($"Ingredient {ingredient.IngredientName} already exists in the master list.");
                        //}
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return;

        }

        public bool RemoveIngredientFromRecipe(int recipeId, int ingredientId)
        {
            bool result = false;
            string sqlDeleteIngredientFromRecipe = "DELETE FROM recipes_ingredients WHERE ingredient_id = @ingredientId AND user_recipe_id = @recipeId";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    Console.WriteLine($"Removing ingredients. RecipeId: {recipeId}, IngredientId: {ingredientId}");


                    SqlCommand cmd = new SqlCommand(sqlDeleteIngredientFromRecipe, conn);
                    cmd.Parameters.AddWithValue("@recipeId", recipeId);
                    cmd.Parameters.AddWithValue("@ingredientId", ingredientId);
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        result = true;
                    }
                }

            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return result;
        }


        private Recipe MapRowToRecipe(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            recipe.RecipeName = Convert.ToString(reader["recipe_name"]);
            recipe.RecipeInstructions = Convert.ToString(reader["recipe_instructions"]);
            return recipe;
        }
        private Recipe MapRowToUserRecipe(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.RecipeId = Convert.ToInt32(reader["user_recipe_id"]);
            recipe.RecipeName = Convert.ToString(reader["recipe_name"]);
            recipe.RecipeInstructions = Convert.ToString(reader["recipe_instructions"]);
            return recipe;
        }

        private Ingredient MapRowToIngredient(SqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.IngredientId = Convert.ToInt32(reader["ingredient_id"]);
            ingredient.IngredientName = Convert.ToString(reader["ingredient_name"]);
            return ingredient;
        }
    }
}
