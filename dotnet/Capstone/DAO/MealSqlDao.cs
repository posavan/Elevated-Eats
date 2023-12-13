using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Capstone.DAO
{
    public class MealSqlDao : IMealDao
    {
        private readonly string connectionString;
        private readonly RecipeSqlDao recipeDao;


        public MealSqlDao(string dbConnectionString)
        {

            this.connectionString = dbConnectionString;
            this.recipeDao = new RecipeSqlDao(dbConnectionString);
        }


        public List<Meal> ListMeals()
        {
            List<Meal> meals = new List<Meal>();

            string sql = "SELECT meal_id, meal_name, meal_description, meal_image FROM meals;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                Meal meal = MapRowToMeal(reader);
                                meal.RecipeList = GetRecipesByMealId(meal.MealId);
                                meals.Add(meal);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return meals;
        }
        public Meal GetMeal(int mealId)
        {
            Meal meal = null;

            string sql = " SELECT meal_id, meal_name, meal_description, meal_image " +
                         "FROM meals " +
                         "WHERE meal_id = @meal_id ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_id", mealId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                meal = MapRowToMeal(reader);
                                meal.RecipeList = GetRecipesByMealId(meal.MealId);
                               
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return meal;
        }

        public Meal CreateMeal(Meal newMeal)
        {
            newMeal.MealId = 0;

            string sql = "INSERT INTO meals (meal_name, meal_description, meal_image) " +
                         "OUTPUT INSERTED.meal_id " +
                         "VALUES (@meal_name, @meal_description, @meal_image) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_name", newMeal.MealName);
                        cmd.Parameters.AddWithValue("@meal_description", newMeal.MealDescription);
                        cmd.Parameters.AddWithValue("@meal_image", newMeal.MealImage);
                        newMeal.MealId = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newMeal;
        }

        public Meal UpdateMeal(Meal updatedMeal)
        {
            string sql = "UPDATE meals " +
                         "SET meal_name = @meal_name, meal_description = @meal_description,  " +
                         "meal_image = @meal_image "+
                         "WHERE meal_id = @meal_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_name", updatedMeal.MealName);
                        cmd.Parameters.AddWithValue("@meal_description", updatedMeal.MealDescription);
                        cmd.Parameters.AddWithValue("@meal_id", updatedMeal.MealId);
                        cmd.Parameters.AddWithValue("@meal_image", updatedMeal.MealImage);

                        int count = cmd.ExecuteNonQuery();

                        if (count == 1)
                        {
                            return updatedMeal;
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
        }

        public bool AddRecipeToMeal(int mealId, Recipe recipe)
        {
            bool result = false;

            string checkSql = "SELECT COUNT(*) meal_recipes  FROM meals_recipes WHERE meal_id=@meal_id AND recipe_id=@recipe_id";

            string sql = "INSERT INTO meals_recipes (meal_id, recipe_id) " +
             "VALUES (@meal_id, @recipe_id);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(checkSql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        return result;
                    }

                    cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);

                    count = Convert.ToInt32(cmd.ExecuteScalar());

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
        public bool RemoveRecipeFromMeal(int mealId, int recipeId)
        {
            bool result = false;
            string sql = "DELETE FROM meals_recipes " +
             "WHERE meal_id = @meal_id AND recipe_id = @recipe_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
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
        public bool DeleteMeal(int mealId)
        {
            bool result = false;
            string mealsRecipesSql = "DELETE FROM meals_recipes WHERE meal_id = @meal_id";
            string mealplansMealsSql = "DELETE FROM meal_plans_meals WHERE meal_id = @meal_id";
            string sql = "DELETE FROM meals WHERE meal_id = @meal_id ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(mealsRecipesSql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    int count = cmd.ExecuteNonQuery();
                    
                    cmd = new SqlCommand(mealplansMealsSql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    count = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
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
        public List<Recipe> GetRecipesByMealId(int mealId)
        {
            List<Recipe> recipes = new List<Recipe>();

            string sql = "SELECT r.recipe_id, r.recipe_name, r.recipe_instructions, r.recipe_image " +
            "FROM recipes r JOIN meals_recipes mr ON r.recipe_id = mr.recipe_id " +
            "WHERE mr.meal_id = @meal_id;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Recipe recipe = recipeDao.MapRowToRecipe(reader);
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

        

        public Meal MapRowToMeal(SqlDataReader reader)
        {
            Meal meal = new Meal();
            meal.MealId = Convert.ToInt32(reader["meal_id"]);
            meal.MealName = Convert.ToString(reader["meal_name"]);
            meal.MealDescription = Convert.ToString(reader["meal_description"]);
            meal.MealImage = Convert.ToString(reader["meal_image"]);
            return meal;
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

    }

}

