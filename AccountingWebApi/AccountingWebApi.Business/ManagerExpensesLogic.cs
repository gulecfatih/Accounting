using AccountingWebApi.Data;
using AccountingWebApi.Data.Models;
using AccountingWebApi.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Business
{
    public class ManagerExpensesLogic
    {
        public readonly AppDbContext _context;

        public ManagerExpensesLogic(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Kullanıcıların Eklediği Masrafları Görür
        /// </summary>
        /// <returns></returns>
        public async Task<List<Expenses>> Get()
        {
            ManagerExpensesRepo expensesRepo = new ManagerExpensesRepo(_context);

            return await expensesRepo.Get();
        }

        public async Task<List<Expenses>> GetApproved()
        {
            ManagerExpensesRepo expensesRepo = new ManagerExpensesRepo(_context);

            return await expensesRepo.GetApproved();
        }


        /// <summary>
        /// Kullanıcın Masraflarına Yöneticisi Onay Vermeden Önce Logic İşlemler Varsa Yapılır
        /// </summary>
        /// <param name="listExpenses"></param>
        /// <returns></returns>
        public async Task<int> Update(List<Expenses> listExpenses)
        {
            ManagerExpensesRepo expensesRepo = new ManagerExpensesRepo(_context);

            return await expensesRepo.Update(listExpenses);
        }

    }

  

}
