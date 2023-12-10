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
        private readonly RecipeSqlDao recipeSqlDao;


        public MealSqlDao(string dbConnectionString)
        {

            this.connectionString = dbConnectionString;
            this.recipeSqlDao = new RecipeSqlDao(dbConnectionString);
        }


        public List<Meal> ListMeals()
        {
            List<Meal> meals = new List<Meal>();

            string sql = "SELECT meal_id, meal_name, meal_description, recipe_id, recipe_name FROM meals;";

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
        public Meal GetMeal(int MealId)
        {
            Meal meal = null;

            string sql = " SELECT meal_id, meal_name, meal_description, recipe_id, recipe_name " +
                         "FROM meals " +
                         "WHERE meal_id = @mealId ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_id", MealId);

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

            string sql = "INSERT INTO meals (meal_name, meal_description, recipe_id) " +
                         "OUTPUT INSERTED.meal_id " +
                         "VALUES (@meal_name, @meal_description, @recipe_id) ";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_name", newMeal.MealName);
                        cmd.Parameters.AddWithValue("@meal_description", newMeal.MealDescription);
                        cmd.Parameters.AddWithValue("@recipe_id", newMeal.RecipeId);
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
                         "SET meal_name = @meal_name, meal_description = @meal_description , recipe_id = @recipe_id " +
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
                        cmd.Parameters.AddWithValue("@recipe_id", updatedMeal.RecipeId);
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



        public bool DeleteMeal(int mealId)
        {
            string sql = "DELETE FROM meals " +
                         "WHERE meal_id = @meal_id ";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_id", mealId);

                        int count = cmd.ExecuteNonQuery();
                        if (count == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
        }



        private Meal MapRowToMeal(SqlDataReader reader)
        {
            Meal meal = new Meal();
            meal.MealId = Convert.ToInt32(reader["meal_id"]);
            meal.MealName = Convert.ToString(reader["meal_name"]);
            meal.MealDescription = Convert.ToString(reader["meal_description"]);
            meal.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            meal.RecipeName = Convert.ToString(reader["recipe_name"]);
            return meal;
        }

    }

}

