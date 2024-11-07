using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class RoomDAO(AppDbContext appDbContext) : IDataAccessObject<Room>
	{
		private readonly AppDbContext _appDbContext = appDbContext;

		public void Add(Room entity)
		{
			_appDbContext.Rooms.Add(entity);
			_appDbContext.SaveChanges();
		}

		public void Delete(Room entity)
		{
			_appDbContext.Rooms.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Room> GetAll()
        {
            return _appDbContext.Rooms.Include(r => r.RoomType);
        }

		public void Update(Room entity)
		{
			_appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_appDbContext.SaveChanges();
		}
	}
}
