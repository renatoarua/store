using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "12345678911")
                return new Customer("Renato Miranda", "re@gmail.com");

            return null;
        }
    }
}
