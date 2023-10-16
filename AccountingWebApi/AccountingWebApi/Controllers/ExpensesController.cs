using AccountingWebApi.Business;
using AccountingWebApi.Data;
using AccountingWebApi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        public readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<Expenses>> Add(Expenses expense)
        {
                ExpensesLogic expensesLogic = new ExpensesLogic(_context);

            var result = await expensesLogic.Add(expense);

            if (result == 1)
            {
                return Ok(expense);
            }
            return BadRequest();

        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<List<Expenses>>> GetLessonItem(string UserId)
        {
            ExpensesLogic expensesLogic = new ExpensesLogic(_context);

            var result = await expensesLogic.Get(UserId);

            return Ok(result);
        }
    }
}
