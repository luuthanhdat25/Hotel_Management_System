using BusinessObjects;

namespace DataAccess.Repository.Interface
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public IEnumerable<Customer> GetActiveList();

        public IEnumerable<Customer> FindActiveByName(string name);

        public IEnumerable<Customer> FindByName(string name);
    }
}
