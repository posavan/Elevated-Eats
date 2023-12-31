﻿using Capstone.Exceptions;
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
            this.connectionString = dbConnectionString;
        }

        public IList<Ingredient> GetIngredients()
        {
            IList<Ingredient> ingredients = new List<Ingredient>();

            string sql = "SELECT ingredient_id, ingredient_name FROM ingredients;";

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

        public bool IngredientExists(Ingredient ingredient)
        {
            string sql = "SELECT COUNT(*) FROM ingredients WHERE ingredient_name = @ingredientName";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ingredientName", ingredient.IngredientName);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count == 1)
                    {
                        return true;

                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DaoException("SQL exception occurred", ex);
            }
            return false;
        }

        public Ingredient GetIngredientById(int ingredientId)
        {
            Ingredient ingredient = null;

            string sql = "SELECT ingredient_id, ingredient_name FROM ingredients WHERE ingredient_id = @ingredient_id";

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

            string sql = "SELECT ingredient_id, ingredient_name FROM ingredients WHERE ingredient_name = @name";

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
            string check = "SELECT * FROM ingredients " +
                "WHERE ingredient_name=@name;";

            string sql = "INSERT INTO ingredients (ingredient_name) " +
                         "OUTPUT INSERTED.ingredient_id " +
                         "VALUES (@name);";

            ingredient.IngredientId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(check, conn);
                    cmd.Parameters.AddWithValue("@name", ingredient.IngredientName);
                    int rowsReturned = Convert.ToInt32(cmd.ExecuteScalar());
                    if (rowsReturned > 0)
                    {
                        return newIngredient;
                    }
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@name", ingredient.IngredientName);
                    ingredient.IngredientId = Convert.ToInt32(cmd.ExecuteScalar());

                }
                newIngredient = GetIngredientById(ingredient.IngredientId);
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
            return ingredient;
        }
    }
}
