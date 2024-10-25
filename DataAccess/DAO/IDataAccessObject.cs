using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	public interface IDataAccessObject<T> where T : class
	{
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		IEnumerable<T> GetAll();
	}
}
