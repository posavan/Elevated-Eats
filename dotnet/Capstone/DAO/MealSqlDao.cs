using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

            string sql = "SELECT meal_id, meal_name, meal_description FROM meals;";

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
                                Meal meal = new Meal();
                                meal = MapRowToMeal(reader);
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

            string sql = " SELECT meal_id, meal_name, meal_description " +
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

            string sql = "INSERT INTO meals (meal_name, meal_description) " +
                         "OUTPUT INSERTED.meal_id " +
                         "VALUES (@meal_name, @meal_description) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_name", newMeal.MealName);
                        cmd.Parameters.AddWithValue("@meal_description", newMeal.MealDescription);
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
                         "SET meal_name = @meal_name, meal_description = @meal_description  " +
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

        public bool AddRecipeToMeal(int mealId, int recipeId)
        {
            bool result = false;

            string checkSql = "SELECT recipe_id FROM meals_recipes WHERE meal_id=@meal_id";
            string sql = "INSERT INTO meals_recipes (meal_id, recipe_id) " +
             "VALUES (@meal_id, @recipe_id);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(checkSql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        return result;
                    }

                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
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
            string sql = "SELECT r.recipe_id, r.recipe_name, r.recipe_instructions " +
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
            return meal;
        }

    }

}

