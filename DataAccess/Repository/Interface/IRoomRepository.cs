using BusinessObjects;

namespace DataAccess.Repository.Interface
{
    public interface IRoomRepository : IRepository<Room>
    {
        public Room? GetById(int id);

        public IEnumerable<Room> GetAllActive();

        public IEnumerable<Room> GetAllByRoomName(string roomName);
    }
}
