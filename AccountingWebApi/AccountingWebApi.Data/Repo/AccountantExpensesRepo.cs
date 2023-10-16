using AccountingWebApi.Data.Logic;
using AccountingWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingWebApi.Data.Repo
{
    public class AccountantExpensesRepo
    {
        public readonly AppDbContext _context;


        public AccountantExpensesRepo(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Tüm Onaylanmış Ama Paid Tiki Atılmamış İşleri Veri Tabanından Çeker
        /// </summary>
        /// <returns></returns>
        public async Task<List<Expenses>> Get()
        {
            var result = await _context.Expenses
           .Where(x => x.Paid == false && x.Approved==true).ToListAsync();
            return result;
        }
        public async Task<List<Expenses>> GetPaid()
        {
            var result = await _context.Expenses
           .Where(x => x.Paid == true && x.Approved == true).ToListAsync();
            return result;
        }

        /// <summary>
        /// Repoda Muhasebecinin Ödendi Tikini Attıktan Sonra Veri Tabanında İşlenmesini sağlar
        /// </summary>
        /// <param name="listExpenses"></param>
        /// <returns></returns>
        public async Task<int> Update(List<Expenses> listExpenses)
        {

            foreach (var updatedExpense in listExpenses)
            {
                var expense = _context.Expenses.FirstOrDefault(e => e.Id == updatedExpense.Id);

                if (expense != null)
                {
                    
                    if(!expense.Paid && updatedExpense.Paid)
                    {
                        expense.Paid = updatedExpense.Paid;
                        //MailSend(expense.UserId, expense.Id); // Main gönderme işleminden önce Mail Ayarları Ayarlanmalı 
                    }                                         // Sıkıntı Olursa MailSend Metodunu Kapatarak işlemi gerçekleştire bilirsiniz 

                }
            }
            var result = await _context.SaveChangesAsync();
            return result;
        }

        /// <summary>
        /// Mail Gönderme İşine Yarıyan Fonksiyon
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        public void MailSend(string userId,int id)
        {
            // mail adresini burdan alıcaksın 
            var user = _context.Users.Where(x => x.Id == userId).ToList().FirstOrDefault();

            var Expenses = _context.Expenses.Where(x => x.Id == id).ToList().FirstOrDefault();
            // konu burdan alıcaksın 
            string subject = "About expense number " + Expenses.ExpenseNumber.ToString() + " expense";
            string body = "Your expense with expense number " + Expenses.ExpenseNumber.ToString() + " has been paid";
            var emailService = new EmailServiceLogic();
            emailService.SendEmail(user.Email, subject, body);
        }

    }
}
