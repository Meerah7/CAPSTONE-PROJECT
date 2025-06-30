// -------------------------------------------
// Data/FinanceRepository.cs
// -------------------------------------------

using finance.Models;
using Microsoft.Data.SqlClient;
using finance.Exceptions;
using System;
using System.Collections.Generic;
using finance.data;

namespace finance.data.dao
{
    public class FinanceDao : IFinanceDao

    {
        public bool CreateUser(User user)
        {
           
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Username and Password cannot be empty.");

            using SqlConnection conn = DbConnectionHelper.GetConnection();
            string sql = "INSERT INTO Userer (Username, Password, Email) VALUES (@username, @password, @email)";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);

            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                    throw new Exception("Failed to create user.");
                return true;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error during user creation: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected error in CreateUser: " + ex.Message, ex);
            }
        }

        public bool CreateExpense(Expense expense)
        {
            if (expense.Amount <= 0)
                throw new InvalidTransactionAmountException("Expense amount must be greater than zero.");

            using SqlConnection conn = DbConnectionHelper.GetConnection();
            string query = @"INSERT INTO Expenses (user_id, amount, category_id, date, description)
                             VALUES (@userId, @amount, @categoryId, @date, @description)";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@userId", expense.UserId);
            cmd.Parameters.AddWithValue("@amount", expense.Amount);
            cmd.Parameters.AddWithValue("@categoryId", expense.CategoryId);
            cmd.Parameters.AddWithValue("@date", expense.Date);
            cmd.Parameters.AddWithValue("@description", string.IsNullOrWhiteSpace(expense.Description) ? DBNull.Value : expense.Description);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
                throw new Exception("Failed to create expense.");

            return true;
        }

        public List<Expense> GetAllExpenses(int userId)
        {
            var expenses = new List<Expense>();
            using SqlConnection conn = DbConnectionHelper.GetConnection();
            string query = "SELECT * FROM Expenses WHERE user_id = @userId";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                expenses.Add(new Expense
                {
                    ExpenseId = (int)reader["expense_id"],
                    UserId = (int)reader["user_id"],
                    Amount = (decimal)reader["amount"],
                    CategoryId = (int)reader["category_id"],
                    Date = (DateTime)reader["date"],
                    Description = reader["description"] == DBNull.Value ? null : reader["description"].ToString()
                });
            }

            if (expenses.Count == 0)
                throw new ExpenseNotFoundException($"No expenses found for user ID {userId}.");

            return expenses;
        }

        public bool UpdateExpense(int userId, Expense updatedExpense)
        {
            if (updatedExpense.Amount <= 0)
                throw new InvalidTransactionAmountException("Expense amount must be greater than zero.");

            using SqlConnection conn = DbConnectionHelper.GetConnection();
            string query = @"UPDATE Expenses SET amount = @amount, category_id = @categoryId,
                             date = @date, description = @description
                             WHERE expense_id = @expenseId AND user_id = @userId";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@amount", updatedExpense.Amount);
            cmd.Parameters.AddWithValue("@categoryId", updatedExpense.CategoryId);
            cmd.Parameters.AddWithValue("@date", updatedExpense.Date);
            cmd.Parameters.AddWithValue("@description", string.IsNullOrWhiteSpace(updatedExpense.Description) ? DBNull.Value : updatedExpense.Description);
            cmd.Parameters.AddWithValue("@expenseId", updatedExpense.ExpenseId);
            cmd.Parameters.AddWithValue("@userId", userId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
                throw new ExpenseNotFoundException($"Expense with ID {updatedExpense.ExpenseId} for user {userId} not found.");

            return true;
        }

        public bool DeleteUser(int userId)
        {
            using SqlConnection conn = DbConnectionHelper.GetConnection();
            string query = "DELETE FROM Users WHERE user_id = @userId";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@userId", userId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
                throw new UserNotFoundException($"User with ID {userId} not found.");

            return true;
        }

        public bool DeleteExpense(int expenseId)
        {
            using SqlConnection conn = DbConnectionHelper.GetConnection();
            string query = "DELETE FROM Expenses WHERE expense_id = @expenseId";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@expenseId", expenseId);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected == 0)
                throw new ExpenseNotFoundException($"Expense with ID {expenseId} not found.");

            return true;
        }
    }
}

