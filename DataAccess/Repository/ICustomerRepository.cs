using BusinessObjects;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
	{
		public List<Customer> GetAll();

		public List<Customer> GetActiveList();

		public List<Customer> FindActiveByName(string name);
		
		public List<Customer> FindByName(string name);

		public void UpdateCustomerStatus(Customer customer, CustomerStatus customerStatus);

		public void Update(Customer customer);

		public Customer FindByEmailAndPassword(string email, string password);

		public Customer FindByEmail(string email);

		public bool IsAdmin(string email, string password);

		public void Add(Customer customer);
	}
}
