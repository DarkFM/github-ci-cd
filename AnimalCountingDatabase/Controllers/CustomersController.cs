using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalCountingDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController
    {
        private readonly CustomerContext context;

        public CustomersController(CustomerContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAll() =>
            await context.Customers.ToArrayAsync();

        [HttpPost]
        public async Task<Customer> Add(Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return customer;
        }
    }
}