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

        // left off here
        public Meal AddAMeal(Meal newMeal)
        {
            newMeal.MealId = 0;

            string sql = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@meal_name", newMeal.MealName);
                        cmd.Parameters.AddWithValue("@meal_description", newMeal.MealDescription);
                        newMeal.MealId= (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex); throw new DaoException("SQL exception occurred", ex);
            }
            return newMeal;
        }










        public List<Meal> ListMealsById(int mealId)
        {
            throw new NotImplementedException();
        }

        public Meal UpdateMeal(Meal newMeal)
        {
            throw new NotImplementedException();
        }



        public bool DeleteAMeal(int mealId)
        {
            throw new NotImplementedException();
        }




        //public meal UpdatePet(meal updatedPet)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = new SqlCommand(SqlUpdatePet, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@name", updatedPet.Name);
        //            cmd.Parameters.AddWithValue("@age", updatedPet.Age);
        //            cmd.Parameters.AddWithValue("@type", updatedPet.Type);
        //            cmd.Parameters.AddWithValue("@id", updatedPet.Id);
        //            cmd.Parameters.AddWithValue("@owner", updatedPet.Owner);

        //            int count = cmd.ExecuteNonQuery();
        //            if (count == 1)
        //            {
        //                return updatedPet;
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //}

        //public bool DeleteAPet(int petId)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = new SqlCommand(SqlDeletePet, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@id", petId);

        //            int count = cmd.ExecuteNonQuery();
        //            if (count == 1)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //}

        public Meal MapRowToMeal(SqlDataReader reader)
        {
            Meal meal = new Meal();
            meal.MealId = Convert.ToInt32(reader["meal_id"]);
            meal.MealName = Convert.ToString(reader["meal_name"]);
            meal.MealDescription = Convert.ToString(reader["description"]);
            meal.RecipeId = Convert.ToInt32(reader["recipe_id"]);
            meal.RecipeName = Convert.ToString(reader["recipe_name"]);
            return meal;
        }

    }

}

