using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using finance.Models;
using finance.data.dao;

namespace finance.data.dao

{
    public interface IFinanceDao
    {
        bool CreateUser(User user);
        bool CreateExpense(Expense expense);
        List<Expense> GetAllExpenses(int userId);
        bool UpdateExpense(int userId, Expense updatedExpense);
        bool DeleteUser(int userId);
        bool DeleteExpense(int expenseId);
    }
}
