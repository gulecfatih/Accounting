using AccountingWebApi.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Data.Repo
{
    public class ExpensesRepo
    {
        public readonly AppDbContext _context;


        public ExpensesRepo(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        ///Masraf Eklenmesini Sağlar
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public async Task<int> Add(Expenses expense)
        {
            _context.Expenses.Add(expense);
            var result = await _context.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Personele Ait Tüm Masrafları Gösterir
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<Expenses>> Get(string id)
        {
            var result = await _context.Expenses
           .Where(x => x.UserId == id).ToListAsync();

            return result;
        }
    }
}
