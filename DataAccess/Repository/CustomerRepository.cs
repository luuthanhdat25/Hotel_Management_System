using BusinessObject;
using BusinessObjects;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class CustomerRepository(CustomerDAO customerDAO) : ICustomerRepository
	{
		private readonly CustomerDAO _customerDAO = customerDAO;

        public void Add(Customer customer)
        {
            _customerDAO.Add(customer);
        }

        public void Delete(Customer entity)
        {
            _customerDAO.Delete(entity);
        }

        public IEnumerable<Customer> GetAllActiveByName(string name)
		{
			return (from customer in _customerDAO.GetAll() 
					where customer.CustomerFullName.ToLower().Contains(name.ToLower())	&& customer.Account.AccountStatus == AccountStatus.Active
					select customer);
		}

        public IEnumerable<Customer> GetAllByName(string name)
        {
            return (from customer in _customerDAO.GetAll()
                    where customer.CustomerFullName.ToLower().Contains(name.ToLower())
                    select customer);
        }

        public IEnumerable<Customer> GetActiveList()
        {
            return (from customer in _customerDAO.GetAll()
                    where customer.Account.AccountStatus == AccountStatus.Active
                    select customer);
        }

        public IEnumerable<Customer> GetAll()
		{
			return _customerDAO.GetAll();
		}

        public void Update(Customer customer)
        {
            _customerDAO.Update(customer);
        }

        public Customer? GetByAccountId(int accountId)
        {
            return _customerDAO.GetAll().FirstOrDefault(customer => customer.AccountId == accountId);
        }
    }
}
