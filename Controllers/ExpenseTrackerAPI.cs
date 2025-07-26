using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseDbContext _context;
        public ExpensesController(ExpenseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_context.Expenses.ToList());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var expense = _context.Expenses.Find(id);
            return expense == null ? NotFound() : Ok(expense);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = expense.Id }, expense);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Expense updated)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null) return NotFound();

            expense.Title = updated.Title;
            expense.Amount = updated.Amount;
            expense.Category = updated.Category;
            expense.Date = updated.Date;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null) return NotFound();

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return NoContent();
        }
    }
}