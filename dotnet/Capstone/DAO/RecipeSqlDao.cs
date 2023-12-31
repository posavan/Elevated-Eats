﻿using Capstone.Exceptions;
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

            string sql = "SELECT recipe_id, recipe_name, recipe_instructions, recipe_image FROM recipes;";


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

            string sql = "SELECT recipe_id, recipe_name, recipe_instructions, recipe_image FROM recipes " +
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

        public Recipe GetUserRecipeById(int recipeId)
        {
            Recipe recipe = null;

            string sql = "SELECT recipe_id, recipe_name, recipe_instructions, recipe_image FROM recipes " +
                "WHERE recipe_id = @recipe_id";

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

        // retrieves from users saved recipes
        //get rid of this method -- duplicate
        public Recipe GetRecipeById(int recipeId)
        {
            Recipe recipe = null;

            string sql = "SELECT recipe_id, recipe_name, recipe_instructions, recipe_image FROM recipes " +
                "WHERE recipe_id = @recipe_id";

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

        public Recipe GetRecipeByName(string recipeName)
        {
            Recipe recipe = null;

            string sql = "SELECT recipe_id, recipe_name, recipe_instructions, recipe_image FROM recipes " +
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

            string sql = "SELECT i.ingredient_id, ingredient_name, ri.quantity FROM ingredients i " +
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

        public Recipe AddRecipeToUser(Recipe recipe, int userId)
        {
            Recipe newRecipe = null;
            string sql = "INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) " +
                         "VALUES (@user_id, @recipe_name, @recipe_instructions, @recipe_image);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@recipe_instructions", recipe.RecipeInstructions);
                    cmd.Parameters.AddWithValue("@recipe_image", recipe.RecipeImage);
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

        public bool RemoveRecipeFromUser(int recipeId, int userId)
        {
            bool result = false;
            string priorASql = "DELETE FROM recipes_ingredients WHERE recipe_id IN " +
                "(SELECT ri.recipe_id FROM recipes_ingredients ri " +
                "JOIN recipes r ON ri.recipe_id=r.recipe_id " +
                "WHERE r.recipe_id=@recipe_id);";
            string priorBSql = "DELETE FROM meals_recipes WHERE recipe_id IN " +
                "(SELECT mr.recipe_id FROM meals_recipes mr "+
                "JOIN recipes r ON mr.recipe_id=r.recipe_id " +
                "WHERE r.recipe_id=@recipe_id);";
            string sql = "DELETE FROM recipes WHERE recipe_id=@recipe_id AND user_id=@user_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(priorASql, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
                    int count = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(priorBSql, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
                    count = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
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

            string sql = "INSERT INTO recipes (user_id, recipe_name, recipe_instructions, recipe_image) " +
                         "OUTPUT INSERTED.recipe_id " +
                         "VALUES (@user_id, @name, @instructions, @image);";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@name", recipe.RecipeName);
                    cmd.Parameters.AddWithValue("@instructions", recipe.RecipeInstructions);
                    cmd.Parameters.AddWithValue("@image", recipe.RecipeImage);
                    newRecipe = GetRecipeById(Convert.ToInt32(cmd.ExecuteScalar()));
                }

                //newRecipe = GetRecipeById(newRecipe.RecipeId);


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
            string sqlUpdateRecipe = "UPDATE recipes SET recipe_name = @recipe_name, recipe_instructions = @recipe_instructions,  " +
                "recipe_image=@recipe_image "+            
                "WHERE recipe_id = @recipe_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlUpdateRecipe, conn);
                    cmd.Parameters.AddWithValue("@recipe_name", updatedRecipe.RecipeName);
                    cmd.Parameters.AddWithValue("@recipe_instructions", updatedRecipe.RecipeInstructions);
                    cmd.Parameters.AddWithValue("@recipe_id", updatedRecipe.RecipeId);
                    cmd.Parameters.AddWithValue("@recipe_image", updatedRecipe.RecipeImage);

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

        public bool AddIngredientsToRecipe(Recipe recipe)
        {
            bool result = false;
            string checkSql = "SELECT * FROM recipes_ingredients WHERE recipe_id=@recipe_id AND " +
            "ingredient_id=@ingredient_id";

            string sql = "INSERT INTO recipes_ingredients (recipe_id, ingredient_id, quantity) " +
             "VALUES (@recipe_id, @ingredient_id, @quantity);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int count = 0;
                    foreach (Ingredient ingredient in recipe.IngredientList)
                    {
                        SqlCommand cmd = new SqlCommand(checkSql, conn);
                        cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);
                        cmd.Parameters.AddWithValue("@ingredient_id", ingredient.IngredientId);
                        int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());
                        if (rowsAffected > 0)
                        {
                            // already added 
                            continue;
                        }

                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);
                        cmd.Parameters.AddWithValue("@ingredient_id", ingredient.IngredientId);
                        cmd.Parameters.AddWithValue("@quantity", ingredient.Quantity);
                        count += Convert.ToInt32(cmd.ExecuteNonQuery());
                    }
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

        public bool RemoveIngredientFromRecipe(int recipeId, int ingredientId)
        {
            bool result = false;
            string sqlDeleteIngredientFromRecipe = "DELETE FROM recipes_ingredients " +
                "WHERE ingredient_id = @ingredientId AND recipe_id = @recipeId";

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


        public Recipe MapRowToRecipe(SqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            recipe.RecipeName = Convert.ToString(reader["recipe_name"]);
            recipe.RecipeInstructions = Convert.ToString(reader["recipe_instructions"]);
            recipe.RecipeImage = Convert.ToString(reader["recipe_image"]);
            return recipe;
        }

        public Ingredient MapRowToIngredient(SqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.IngredientId = Convert.ToInt32(reader["ingredient_id"]);
            ingredient.IngredientName = Convert.ToString(reader["ingredient_name"]);
            ingredient.Quantity = Convert.ToString(reader["quantity"]);
            return ingredient;
        }
    }
}
