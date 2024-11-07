using BusinessObjects;
using Microsoft.EntityFrameworkCore;

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

		public IEnumerable<Customer> GetAll()
		{
			return _appDbContext.Customers.Include(customer => customer.Account);
		}

		public void Update(Customer entity)
		{
			_appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_appDbContext.SaveChanges();
        }
	}
}
