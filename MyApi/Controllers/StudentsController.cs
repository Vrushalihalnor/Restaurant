// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using MyApi.Data;
// using MyApi.Models;

// namespace MyApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class StudentsController : ControllerBase
//     {
//         private readonly AppDbContext _db;
//         public StudentsController(AppDbContext db) => _db = db;

//         [HttpGet]
//         public async Task<IActionResult> GetAll() =>
//             Ok(await _db.Students.AsNoTracking().ToListAsync());

//         [HttpGet("{id:int}")]
//         public async Task<IActionResult> Get(int id)
//         {
//             var s = await _db.Students.FindAsync(id);
//             if (s == null) return NotFound();
//             return Ok(s);
//         }

//         [HttpPost]
//         public async Task<IActionResult> Create(Student student)
//         {
//             _db.Students.Add(student);
//             await _db.SaveChangesAsync();
//             return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
//         }

//         [HttpPut("{id:int}")]
//         public async Task<IActionResult> Update(int id, Student student)
//         {
//             if (id != student.Id) return BadRequest();
//             var exists = await _db.Students.AnyAsync(x => x.Id == id);
//             if (!exists) return NotFound();

//             _db.Entry(student).State = EntityState.Modified;
//             await _db.SaveChangesAsync();
//             return NoContent();
//         }

//         [HttpDelete("{id:int}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             var s = await _db.Students.FindAsync(id);
//             if (s == null) return NotFound();
//             _db.Students.Remove(s);
//             await _db.SaveChangesAsync();
//             return NoContent();
//         }
//     }
// }
