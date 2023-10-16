using AccountingWebApi.Data;
using AccountingWebApi.Data.Models;
using AccountingWebApi.Data.Repo;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Business
{
    public class ExpensesLogic
    {
        public readonly AppDbContext _context;

        public ExpensesLogic(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        ///  Kullanıcının Eklediği Masrafları Veri Tabanına Atmadan Önce  Logic İşlemler Varsa Yapılır
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public async Task<int> Add(Expenses expense)
        {
            ExpensesRepo expensesRepo = new ExpensesRepo(_context);

            return await expensesRepo.Add(expense);
        }
        /// <summary>
        /// Kullanıcının UserId'si İle Masrafları Çekmeden Önce Logic İşlemler Varsa Yapılır
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Expenses>> Get(string id)
        {
            ExpensesRepo expensesRepo = new ExpensesRepo(_context);

            return await expensesRepo.Get(id);
        }

    }

}
