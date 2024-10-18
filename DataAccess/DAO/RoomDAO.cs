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

        public List<Room> GetAll()
        {
            return _appDbContext.Rooms
                .Include(r => r.RoomType)
                .ToList();
        }

        public Room GetById(int id)
		{
			return (from room in _appDbContext.Rooms
					where room.RoomId == id
					select room).FirstOrDefault();
		}

		public void Update(Room entity)
		{
			_appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_appDbContext.SaveChanges();
		}
	}
}
