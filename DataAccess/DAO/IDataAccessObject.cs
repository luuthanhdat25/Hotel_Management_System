using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
	internal interface IDataAccessObject<T> where T : class
	{
		T GetById(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		List<T> GetAll();
	}
}
