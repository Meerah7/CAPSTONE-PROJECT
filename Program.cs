
using finance.data.dao;
using finance.Models;
using finance.Services;
using System;
using System.Collections.Generic;

namespace finance.Main
{
    class Program
    {
        static void Main()
        {
            IFinanceDao dao = new FinanceDao();
            FinanceService service = new FinanceService(dao);

            while (true)
            {
                Console.WriteLine("\n--- Finance Management System ---");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. Add Expense");
                Console.WriteLine("3. View Expenses");
                Console.WriteLine("4. Update Expense");
                Console.WriteLine("5. Delete Expense");
                Console.WriteLine("6. Delete User");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                string input = Console.ReadLine()?.Trim() ?? "";

                try
                {
                    switch (input)
                    {
                        case "1":
                            Console.Write("Enter username: ");
                            string uname = Console.ReadLine()?.Trim() ?? "";
                            Console.Write("Enter password: ");
                            string pwd = Console.ReadLine()?.Trim() ?? "";
                            Console.Write("Enter email (optional): ");
                            string email = Console.ReadLine()?.Trim() ?? "";

                            var newUser = new User
                            {
                                Username = uname,
                                Password = pwd,
                                Email = string.IsNullOrWhiteSpace(email) ? null : email
                            };

                            service.RegisterUser(newUser);
                            Console.WriteLine("✅ User created successfully!");
                            break;

                        case "2":
                            Console.Write("Enter User ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int userId))
                            {
                                Console.WriteLine("❌ Invalid User ID.");
                                break;
                            }

                            Console.Write("Enter amount: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal amt))
                            {
                                Console.WriteLine("❌ Invalid amount.");
                                break;
                            }

                            Console.Write("Enter category ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int catId))
                            {
                                Console.WriteLine("❌ Invalid category ID.");
                                break;
                            }

                            Console.Write("Enter description (optional): ");
                            string desc = Console.ReadLine()?.Trim() ?? "";

                            var expense = new Expense
                            {
                                UserId = userId,
                                Amount = amt,
                                CategoryId = catId,
                                Date = DateTime.Now,
                                Description = string.IsNullOrWhiteSpace(desc) ? null : desc
                            };

                            service.AddExpense(expense);
                            Console.WriteLine("✅ Expense added successfully!");
                            break;

                        case "3":
                            Console.Write("Enter User ID to view expenses: ");
                            if (!int.TryParse(Console.ReadLine(), out int viewUid))
                            {
                                Console.WriteLine("❌ Invalid User ID.");
                                break;
                            }

                            var expenses = service.ViewExpenses(viewUid);
                            if (expenses.Count == 0)
                            {
                                Console.WriteLine("ℹ️ No expenses found.");
                            }
                            else
                            {
                                Console.WriteLine("\n--- Expenses ---");
                                foreach (var e in expenses)
                                {
                                    Console.WriteLine($"ID: {e.ExpenseId}, ₹{e.Amount}, {e.Description}, {e.Date.ToShortDateString()}");
                                }
                            }
                            break;

                        case "4":
                            Console.Write("Enter your User ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int updateUid))
                            {
                                Console.WriteLine("❌ Invalid User ID.");
                                break;
                            }

                            Console.Write("Enter Expense ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out int expIdToUpdate))
                            {
                                Console.WriteLine("❌ Invalid Expense ID.");
                                break;
                            }

                            Console.Write("Enter new amount: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal newAmt))
                            {
                                Console.WriteLine("❌ Invalid amount.");
                                break;
                            }

                            Console.Write("Enter new category ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int newCat))
                            {
                                Console.WriteLine("❌ Invalid category ID.");
                                break;
                            }

                            Console.Write("Enter new description: ");
                            string newDesc = Console.ReadLine()?.Trim() ?? "";

                            var updated = new Expense
                            {
                                ExpenseId = expIdToUpdate,
                                Amount = newAmt,
                                CategoryId = newCat,
                                Date = DateTime.Now,
                                Description = string.IsNullOrWhiteSpace(newDesc) ? null : newDesc
                            };

                            service.EditExpense(updateUid, updated);
                            Console.WriteLine("✅ Expense updated!");
                            break;

                        case "5":
                            Console.Write("Enter Expense ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out int expId))
                            {
                                Console.WriteLine("❌ Invalid Expense ID.");
                                break;
                            }

                            service.RemoveExpense(expId);
                            Console.WriteLine("✅ Expense deleted.");
                            break;

                        case "6":
                            Console.Write("Enter User ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out int delUser))
                            {
                                Console.WriteLine("❌ Invalid User ID.");
                                break;
                            }

                            service.RemoveUser(delUser);
                            Console.WriteLine("✅ User deleted.");
                            break;

                        case "0":
                            Console.WriteLine("👋 Exiting... Goodbye!");
                            return;

                        default:
                            Console.WriteLine("❌ Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error: {ex.Message}");
                }
            }
        }
    }
}

