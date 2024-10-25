using BusinessObjects;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

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

        public IEnumerable<Room> GetByRoomActiveId(int id)
        {
            return (from room in _roomDAO.GetAll()
                    where room.RoomId == id && room.RoomStatus == RoomStatus.Active
                    select room);
        }

        public IEnumerable<Room> GetAllActive()
        {
            return (from room in _roomDAO.GetAll()
                    where room.RoomStatus == RoomStatus.Active
                    select room);
        }

        public IEnumerable<Room> GetAll()
		{
			return _roomDAO.GetAll();
		}

		public Room? GetById(int id)
		{
			return _roomDAO.GetAll().FirstOrDefault(room => room.RoomId == id);
		}

		public void Update(Room room)
		{
			_roomDAO.Update(room);
		}

        public IEnumerable<Room> GetAllByRoomName(string roomName)
        {
            return _roomDAO.GetAll().Where(room => room.RoomName.ToLower().Equals(roomName.ToLower()));
        }
    }
}
