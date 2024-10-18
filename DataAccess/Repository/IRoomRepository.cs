using BusinessObjects;

namespace DataAccess.Repository
{
	public interface IRoomRepository
	{
		public List<Room> GetAll();
		public Room FindById(int id);

		public void Update(Room room);
		public void Add(Room room);
		public void Delete(Room room);
		public Room GetById(int id);

		public List<Room> GetActiveList();
		
		public List<Room> FindActiveById(int id);
    }
}
