using AccountingWebApi.Business;
using AccountingWebApi.Data;
using AccountingWebApi.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerExpensesController : ControllerBase
    {

        public readonly AppDbContext _context;

        public ManagerExpensesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update(List<Expenses> listExpenses)
        {
            ManagerExpensesLogic managerExpensesLogic = new ManagerExpensesLogic(_context);

            var result = await managerExpensesLogic.Update(listExpenses);

            return Ok(result);
        }


        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<List<Expenses>>> Get()
        {
            ManagerExpensesLogic managerExpensesLogic = new ManagerExpensesLogic(_context);

            var result = await managerExpensesLogic.Get();

            return Ok(result);
        }

        [HttpGet]
        [Route("GetApproved")]
        public async Task<ActionResult<List<Expenses>>> GetApproved()
        {
            ManagerExpensesLogic managerExpensesLogic = new ManagerExpensesLogic(_context);

            var result = await managerExpensesLogic.GetApproved();

            return Ok(result);
        }

    }
}
