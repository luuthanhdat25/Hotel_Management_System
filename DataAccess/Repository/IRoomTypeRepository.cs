using BusinessObject;

namespace DataAccess.Repository
{
	public interface IRoomTypeRepository
	{
		public List<RoomType> GetAll();

		public RoomType GetByName(string name);
	}
}
