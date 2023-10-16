using AccountingWebApi.Business;
using AccountingWebApi.Data;
using AccountingWebApi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountantExpensesController : ControllerBase
    {
        public readonly AppDbContext _context;

        public AccountantExpensesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update(List<Expenses> listExpenses)
        {
            AccountantExpensesLogic accountantExpensesLogic = new AccountantExpensesLogic(_context);

            var result = await accountantExpensesLogic.Update(listExpenses);

            return Ok(result);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<List<Expenses>>> Get()
        {
            AccountantExpensesLogic accountantExpensesLogic = new AccountantExpensesLogic(_context);

            var result = await accountantExpensesLogic.Get();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetPaid")]
        public async Task<ActionResult<List<Expenses>>> GetPaid()
        {
            AccountantExpensesLogic accountantExpensesLogic = new AccountantExpensesLogic(_context);

            var result = await accountantExpensesLogic.GetPaid();

            return Ok(result);
        }

    }
}
