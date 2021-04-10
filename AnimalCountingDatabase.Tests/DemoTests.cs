using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AnimalCountingDatabase.Controllers;
using System.Linq;

namespace AnimalCountingDatabase.Tests
{
    public class DemoTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task CustomerIntegrationTest()
        {
            // Create DB Context
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new CustomerContext(optionsBuilder.Options);

            // Delete all existing customers in the DB
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            // context.Customers.RemoveRange(await context.Customers.ToArrayAsync());
            // await context.SaveChangesAsync();

            // Create Controller
            var controller = new CustomersController(context);

            // Add customer
            await controller.Add(new Customer { CustomerName = "FooBar" });

            // Check: Does GetAll return the added customer?
            var result = (await controller.GetAll()).ToArray();
            Assert.Single(result);
            Assert.Equal("FooBar", result[0].CustomerName);
        }
    }
}
