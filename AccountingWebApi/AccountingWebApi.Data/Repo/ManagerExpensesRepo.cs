using AccountingWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Data.Repo
{
    public class ManagerExpensesRepo
    {
        public readonly AppDbContext _context;


        public ManagerExpensesRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Expenses>> Get()
        {
            var result = await _context.Expenses
           .Where(x => x.Approved == false).ToListAsync();

            return result;
        }

        public async Task<List<Expenses>> GetApproved()
        {
            var result = await _context.Expenses
           .Where(x => x.Approved == true).ToListAsync();

            return result;
        }

        public async Task<int> Update(List<Expenses> listExpenses)
        {

            foreach (var updatedExpense in listExpenses)
            {
                var expense = _context.Expenses.FirstOrDefault(e => e.Id == updatedExpense.Id);

                if (expense != null)
                {
                    expense.Approved = updatedExpense.Approved;
                    
                }
            }

            var result = await _context.SaveChangesAsync();


            return result;

        }
        
    }
}
