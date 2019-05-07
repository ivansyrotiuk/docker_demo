using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace CarRentalService
{
    public interface ICustomersClient
    {
        [Get("api/customers/{id}")]
        Task<Customer> Get([Path]int id);
        [Get("api/customers")]
        Task<IEnumerable<Customer>> GetAll();
    }
}