using AccountingWebApi.Data.Models;
using AccountingWebApi.Data.Repo;
using AccountingWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Business
{
    public class AccountantExpensesLogic
    {
        public readonly AppDbContext _context;

        public AccountantExpensesLogic(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Kullanıcının Masraflarını Çeker
        /// </summary>
        /// <returns></returns>
        public async Task<List<Expenses>> Get()
        {
            AccountantExpensesRepo expensesRepo = new AccountantExpensesRepo(_context);

            return await expensesRepo.Get();
        }
        public async Task<List<Expenses>> GetPaid()
        {
            AccountantExpensesRepo expensesRepo = new AccountantExpensesRepo(_context);

            return await expensesRepo.GetPaid();
        }

        /// <summary>
        /// Kullanıcının Masraflarına Ödendi Tiki Atmadan Önceki İşlemleri Gerçekleştirir
        /// </summary>
        /// <param name="listExpenses"></param>
        /// <returns></returns>
        public async Task<int> Update(List<Expenses> listExpenses)
        {
            AccountantExpensesRepo accountantExpensesRepo = new AccountantExpensesRepo(_context);

            return await accountantExpensesRepo.Update(listExpenses);

        }

    }
}
