using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public OrdersController(AppDbContext db) => _db = db;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Dish)
                .AsNoTracking()
                .ToListAsync();

            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Dish)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound(new { message = "Order not found." });

            return Ok(order);
       }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            if (order == null || order.OrderItems.Count == 0)
                return BadRequest(new { message = "Invalid order data." });

            order.OrderDate = DateTime.Now;
            foreach (var item in order.OrderItems)
            {
                var dish = await _db.Dishes.FindAsync(item.DishId);
                if (dish == null)
                    return BadRequest($"Dish {item.DishId} not found.");

                item.Quantity = item.Quantity <= 0 ? 1 : item.Quantity;
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
     }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
