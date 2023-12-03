using Capstone.Exceptions;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class IngredientSqlDao : IIngredientDao
    {
        private readonly string connectionString;

        public IngredientSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public IList<Ingredient> GetIngredients()
        {
            IList<Ingredient> ingredients = new List<Ingredient>();

            string sql = "SELECT ingredient_id, ingredient_name, calories FROM ingredients";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
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

        public Ingredient GetIngredientById(int ingredientId)
        {
            Ingredient ingredient = null;

            string sql = "SELECT ingredient_id, ingredient_name, calories FROM ingredients WHERE ingredient_id = @ingredient_id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ingredient_id", ingredientId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ingredient = MapRowToIngredient(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return ingredient;
        }

        public Ingredient GetIngredientByName(string name)
        {
            Ingredient ingredient = null;

            string sql = "SELECT ingredient_id, ingredient_name, calories FROM ingredients WHERE ingredient_name = @name";

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
                        ingredient = MapRowToIngredient(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return ingredient;
        }

        public Ingredient CreateIngredient(Ingredient ingredient)
        {
            Ingredient newIngredient = null;

            string sql = "INSERT INTO ingredients (ingredient_name, calories) " +
                         "OUTPUT INSERTED.ingredient_id " +
                         "VALUES (@name, @calories)";

            int newIngredientId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", ingredient.IngredientName);
                    cmd.Parameters.AddWithValue("@calories", ingredient.Calories);
                    newIngredientId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                newIngredient = GetIngredientById(newIngredientId);
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }

            return newIngredient;
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
