using BusinessObjects;

namespace DataAccess.Repository.Interface
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public IEnumerable<Customer> GetActiveList();

        public IEnumerable<Customer> GetAllActiveByName(string name);

        public IEnumerable<Customer> GetAllByName(string name);

        public Customer? GetByAccountId(int accountId);
    }
}
