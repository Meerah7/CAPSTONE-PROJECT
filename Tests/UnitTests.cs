using NUnit.Framework;
using finance.Models;
using finance.data.dao;
using finance.Exceptions;
using System.Collections.Generic;
using System;

namespace finance.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private FinanceDao repo = null!;

        [SetUp]
        public void Setup()
        {
            repo = new FinanceDao();
        }

        // Basic CRUD Tests

        [Test]
        public void TestCreateUser()
        {
            var user = new User { Username = "testuser", Password = "pass123", Email = "testuser@example.com" };
            var result = repo.CreateUser(user);
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestCreateExpense()
        {
            var expense = new Expense
            {
                UserId = 1, // make sure this user exists in your test DB or mock
                Amount = 150.75M,
                CategoryId = 1, // make sure this category exists
                Date = DateTime.Now,
                Description = "Test Expense"
            };
            var result = repo.CreateExpense(expense);
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestGetAllExpenses()
        {
            var expenses = repo.GetAllExpenses(1); // Use an existing user ID
            Assert.That(expenses, Is.Not.Null);
            Assert.That(expenses, Is.InstanceOf<List<Expense>>());
        }

        [Test]
        public void TestUpdateExpense()
        {
            var expense = new Expense
            {
                ExpenseId = 1,  // existing expense ID
                UserId = 1,
                Amount = 200.00M,
                CategoryId = 2,
                Date = DateTime.Now,
                Description = "Updated Expense"
            };
            var result = repo.UpdateExpense(1, expense);
            Assert.That(result, Is.True);
        }

        [Test]
        public void TestDeleteExpense()
        {
            var result = repo.DeleteExpense(1); // Use a valid existing ExpenseId
            Assert.That(result, Is.True);
        }

        // Exception tests

        [Test]
        public void TestDeleteUser_UserNotFound()
        {
            Assert.Throws<UserNotFoundException>(() => repo.DeleteUser(9999));
        }

        [Test]
        public void TestDeleteExpense_ExpenseNotFound()
        {
            Assert.Throws<ExpenseNotFoundException>(() => repo.DeleteExpense(9999));
        }

        // Fake login test

        [Test]
        public void TestFakeUserLogin()
        {
            var user = new User { Username = "testlogin", Password = "pwd123", Email = "login@test.com" };
            repo.CreateUser(user);

            var allExpenses = repo.GetAllExpenses(1); // Example; adjust logic as per your repo
            Assert.That(allExpenses, Is.Not.Null);
        }
    }
}
