using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _context.Cars.ToListAsync();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id);
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _context.Cars.RemoveRange(_context.Cars.Where(c => c.Id == id));
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
