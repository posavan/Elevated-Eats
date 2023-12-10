using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class MealPlanSqlDao : IMealPlanDao
    {
        private readonly string connectionString;
        private readonly MealSqlDao mealDao;

        public MealPlanSqlDao(string dbConnectionString)
        {

            this.connectionString = dbConnectionString;
            this.mealDao = new MealSqlDao(dbConnectionString);
        }

        string getListSql = "SELECT meal_plan_id, meal_plan_name, meal_plan_description " +
            "FROM meal_plans WHERE user_id = @user_id;";
        string getMealPlanSql = "SELECT meal_plan_id, meal_plan_name, meal_plan_description " +
            "FROM meal_plans WHERE meal_plan_id = @meal_plan_id";

        string createSql = "INSERT INTO meal_plans (meal_plan_name, meal_plan_description, user_id) " +
            "OUTPUT INSERTED.meal_plan_id " +
            "VALUES (@name, @description, @user_id);";
        string updateSql = "UPDATE meal_plans " +
            "SET meal_plan_name = @meal_plan_name, meal_plan_description = @meal_plan_description " +
            "WHERE meal_plan_id = @meal_plan_id AND user_id = @user_id";
        string addMealSql = "INSERT INTO meal_plan_meals (meal_plan_id, meal_id) " +
             "VALUES(@meal_plan_id, @meal_id);";
        string removeMealSql = "DELETE FROM meal_plans_meals " +
             "WHERE meal_id = @mealId AND meal_plan_id = @meal_plan_id";

        string deleteMealsSql = "DELETE FROM meal_plans_meals " +
            "WHERE meal_plan_id = @meal_plan_id";
        string deleteSql = "DELETE FROM meal_plans WHERE meal_plan_id=@meal_plan_id;";
        string getMealsSql = "SELECT m.meal_plan_id, m.meal_id, m.meal_name " +
            "FROM meals m JOIN meal_plans_meals mpm ON m.meal_id = mpm.meal_id " +
            "WHERE mpm.meal_plan_id = @meal_plan_id;";


        public List<MealPlan> ListMealPlansByUserId(int userId)
        {
            List<MealPlan> mealplans = new List<MealPlan>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(getListSql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MealPlan mealplan = MapRowToMealPlan(reader);
                        mealplan.MealList = GetMealsByMealPlanId(mealplan.MealPlanId);
                        mealplans.Add(mealplan);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return mealplans;
        }
        public MealPlan GetMealPlanById(int mealPlanId)
        {
            MealPlan mealplan = new MealPlan();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(getMealPlanSql, conn);
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        mealplan = MapRowToMealPlan(reader);
                        mealplan.MealList = GetMealsByMealPlanId(mealplan.MealPlanId);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return mealplan;
        }

        public MealPlan CreateMealPlan(MealPlan mealPlan, int userId)
        {
            MealPlan mealplan = new MealPlan();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(createSql, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@name", mealPlan.MealPlanName);
                    cmd.Parameters.AddWithValue("@instructions", mealPlan.MealPlanDescription);
                    mealplan = GetMealPlanById(Convert.ToInt32(cmd.ExecuteScalar()));
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return mealplan;
        }
        public MealPlan UpdateMealPlan(MealPlan mealPlan, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(updateSql, conn);
                    cmd.Parameters.AddWithValue("@meal_plan_name", mealPlan.MealPlanName);
                    cmd.Parameters.AddWithValue("@meal_plan_description", mealPlan.MealPlanDescription);
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlan.MealPlanId);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    int count = cmd.ExecuteNonQuery();

                    if (count == 1)
                    {
                        return GetMealPlanById(mealPlan.MealPlanId);
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

        public bool AddMealToMealPlan(int mealId, int mealPlanId)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(addMealSql, conn);
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
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
        public bool RemoveMealFromMealPlan(int mealId, int mealPlanId)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(removeMealSql, conn);
                    cmd.Parameters.AddWithValue("@meal_id", mealId);
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);
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

        public bool DeleteMealPlan(int mealPlanId)
        {
            bool result = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(deleteMealsSql, conn);
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);
                    int count = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand(deleteSql, conn);
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);
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

        public List<Meal> GetMealsByMealPlanId(int mealPlanId)
        {
            List<Meal> meals = new List<Meal>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(getMealsSql, conn);
                    cmd.Parameters.AddWithValue("@meal_plan_id", mealPlanId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Meal meal = mealDao.MapRowToMeal(reader);
                        meals.Add(meal);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return meals;
        }
        public MealPlan MapRowToMealPlan(SqlDataReader reader)
        {
            MealPlan mealplan = new MealPlan();
            mealplan.MealPlanId = Convert.ToInt32(reader["meal_plan_id"]);
            mealplan.MealPlanName = Convert.ToString(reader["meal_plan_name"]);
            mealplan.MealPlanDescription = Convert.ToString(reader["meal_plan_description"]);
            mealplan.UserId = Convert.ToInt32(reader["user_id"]);
            return mealplan;
        }
    }
}
