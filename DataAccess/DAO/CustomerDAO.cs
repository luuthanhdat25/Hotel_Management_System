using BusinessObjects;

namespace DataAccess.DAO
{
	public class CustomerDAO(AppDbContext appDbContext) : IDataAccessObject<Customer>
	{
		private readonly AppDbContext _appDbContext = appDbContext;

		public void Add(Customer entity)
		{
			_appDbContext.Customers.Add(entity);
			_appDbContext.SaveChanges();
		}

		public void Delete(Customer entity)
		{
			_appDbContext.Customers.Remove(entity);
            _appDbContext.SaveChanges();
        }

		public List<Customer> GetAll()
		{
			return _appDbContext.Customers.ToList();
		}

		public Customer GetById(int id)
		{
			return (from customer in _appDbContext.Customers
					where customer.CustomerId == id
					select customer).FirstOrDefault();
		}

		public void Update(Customer entity)
		{
			_appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_appDbContext.SaveChanges();
        }
	}
}
