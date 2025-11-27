using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public DishesController(AppDbContext db) => _db = db;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dishes = await _db.Dishes.AsNoTracking().ToListAsync();
            return Ok(dishes);
       }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dish = await _db.Dishes.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
            if (dish == null) return NotFound(new { message = "Dish not found." });
            return Ok(dish);
       }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dish dish)
        {
            if (dish == null) return BadRequest(new { message = "Invalid dish data." });

            _db.Dishes.Add(dish);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dish.Id }, dish);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Dish updatedDish)
        {
            if (updatedDish == null) return BadRequest(new { message = "Invalid dish data." });

            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFound(new { message = "Dish not found." });

            dish.Name = updatedDish.Name;
            dish.Price = updatedDish.Price;
            dish.Category = updatedDish.Category;
            dish.IsAvailable = updatedDish.IsAvailable;

            await _db.SaveChangesAsync();
            return NoContent();
}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFound(new { message = "Dish not found." });

            _db.Dishes.Remove(dish);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
