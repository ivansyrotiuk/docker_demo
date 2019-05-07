using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CarRentalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMongoCollection<Order> _ordersCollection;
        private readonly ICarsClient _carsClient;
        private readonly ICustomersClient _customersClient;
        public OrdersController(IConfiguration config, ICarsClient carsClient, ICustomersClient customersClient)
        {
            _carsClient = carsClient;
            _customersClient = customersClient;
            var client = new MongoClient(config.GetConnectionString("OrdersDb"));
            var database = client.GetDatabase("Orders");
            _ordersCollection = database.GetCollection<Order>("Orders");
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _carsClient.GetAll();
            var customers = await _customersClient.GetAll();
            var orders = await _ordersCollection.FindSync(o => true).ToListAsync();
            var response = orders.Select(o => new OrderModel
            {
                Id = o.Id,
                Customer = customers.Where(c => c.Id == o.CustomerId).Select(c => c.Name).FirstOr(string.Empty),
                Car = cars.Where(c => c.Id == o.CarId).Select(c => c.Name).FirstOr(string.Empty)
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var order = await _ordersCollection.FindSync(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null)
                return NotFound();

            var car = await _carsClient.Get(order.CarId);
            var customer = await _customersClient.Get(order.CustomerId);

            var response = new OrderModel
            {
                Id = order.Id,
                Customer = customer.Name,
                Car = car.Name
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            await _ordersCollection.InsertOneAsync(order);
            return Ok();
        }
    }
}
