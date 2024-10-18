using BusinessObjects;
using DataAccess.DAO;

namespace DataAccess.Repository
{
	public class RoomRepository(RoomDAO roomDAO) : IRoomRepository
	{
		private readonly RoomDAO _roomDAO = roomDAO;

		public void Add(Room room)
		{
			_roomDAO.Add(room);
		}

		public void Delete(Room room)
		{
			_roomDAO.Delete(room);
		}

        public List<Room> FindActiveById(int id)
        {
            return (from room in _roomDAO.GetAll()
                    where room.RoomId == id && room.RoomStatus == RoomStatus.Active
                    select room).ToList();
        }

        public Room FindById(int id)
		{
			return _roomDAO.GetById(id);
		}

        public List<Room> GetActiveList()
        {
            return (from room in _roomDAO.GetAll()
                    where room.RoomStatus == RoomStatus.Active
                    select room).ToList();
        }

        public List<Room> GetAll()
		{
			return _roomDAO.GetAll();
		}

		public Room GetById(int id)
		{
			return _roomDAO.GetById(id);
		}

		public void Update(Room room)
		{
			_roomDAO.Update(room);
		}
	}
}
