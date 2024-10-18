using BusinessObjects;
using DataAccess.DAO;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repository
{
    public class CustomerRepository(CustomerDAO customerDAO) : ICustomerRepository
	{
		private readonly CustomerDAO _customerDAO = customerDAO;

        public void Add(Customer customer)
        {
            _customerDAO.Add(customer);
        }

        public Customer FindByEmail(string email)
        {
            return (from customer in _customerDAO.GetAll()
                    where customer.EmailAddress.ToLower().Equals(email.ToLower())
                    select customer).FirstOrDefault();
        }

        public Customer FindByEmailAndPassword(string email, string password)
        {
            return (from customer in _customerDAO.GetAll()
                    where customer.EmailAddress.ToLower().Equals(email.ToLower()) && customer.Password.Equals(password)
                    select customer).FirstOrDefault();
        }

        public List<Customer> FindActiveByName(string name)
		{
			return (from customer in _customerDAO.GetAll() 
					where customer.CustomerFullName.ToLower().Contains(name.ToLower())	&& customer.CustomerStatus == CustomerStatus.Active
					select customer).ToList();
		}

        public List<Customer> GetActiveList()
        {
            return (from customer in _customerDAO.GetAll()
                    where customer.CustomerStatus == CustomerStatus.Active
                    select customer).ToList();
        }

        public List<Customer> GetAll()
		{
			return _customerDAO.GetAll();
		}

        public bool IsAdmin(string email, string password)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            string adminEmail = configuration["AdminAccount:Email"];
            string adminPassword = configuration["AdminAccount:Password"];

            return email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && password.Equals(adminPassword);
        }

        public void Update(Customer customer)
        {
            _customerDAO.Update(customer);
        }

        public void UpdateCustomerStatus(Customer customer, CustomerStatus customerStatus)
		{
			customer.CustomerStatus = customerStatus;
			_customerDAO.Update(customer);
		}

        public List<Customer> FindByName(string name)
        {
            return (from customer in _customerDAO.GetAll()
                    where customer.CustomerFullName.ToLower().Contains(name.ToLower())
                    select customer).ToList();
        }
    }
}
