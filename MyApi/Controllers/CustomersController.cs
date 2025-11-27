using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyRestaurantApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _db;
        public CustomersController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _db.Customers.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            _db.Customers.Add(customer);
            await _db.SaveChangesAsync();
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Customer updatedCustomer)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            customer.Name = updatedCustomer.Name;
            customer.Email = updatedCustomer.Email;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _db.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            _db.Customers.Remove(customer);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
