using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using finance.data.dao;
using finance.Models;


namespace finance.Services
{
    public class FinanceService
    {
        private readonly IFinanceDao _dao;

        public FinanceService(IFinanceDao dao)
        {
            _dao = dao;
        }

        public void RegisterUser(User user)
        {
            // add extra validation or logic if needed
            _dao.CreateUser(user);
        }

        public void AddExpense(Expense expense)
        {
            _dao.CreateExpense(expense);
        }

        public List<Expense> ViewExpenses(int userId)
        {
            return _dao.GetAllExpenses(userId);
        }

        public void EditExpense(int userId, Expense expense)
        {
            _dao.UpdateExpense(userId, expense);
        }

        public void RemoveUser(int userId)
        {
            _dao.DeleteUser(userId);
        }

        public void RemoveExpense(int expenseId)
        {
            _dao.DeleteExpense(expenseId);
        }
    }
}

