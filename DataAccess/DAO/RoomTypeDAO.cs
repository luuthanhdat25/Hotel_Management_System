using BusinessObject;

namespace DataAccess.DAO
{
	public class RoomTypeDAO(AppDbContext appDbContext) : IDataAccessObject<RoomType>
	{
		private readonly AppDbContext _appDbContext = appDbContext;

		public void Add(RoomType entity)
		{
			_appDbContext.RoomTypes.Add(entity);
			_appDbContext.SaveChanges();
		}

		public void Delete(RoomType entity)
		{
			_appDbContext.RoomTypes.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<RoomType> GetAll()
		{
			return _appDbContext.RoomTypes;
		}

		public void Update(RoomType entity)
		{
			_appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_appDbContext.SaveChanges();
		}
	}
}
