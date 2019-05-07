using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace CarRentalService
{
    public interface ICarsClient
    {
        [Get("api/cars/{id}")]
        Task<Car> Get([Path]int id);
        [Get("api/cars")]
        Task<IEnumerable<Car>> GetAll();
    }
}