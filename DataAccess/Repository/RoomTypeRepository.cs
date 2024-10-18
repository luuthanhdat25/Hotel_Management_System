using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repository
{
	public class RoomTypeRepository(RoomTypeDAO roomTypeDAO) : IRoomTypeRepository
	{
		private readonly RoomTypeDAO _roomTypeDAO = roomTypeDAO;
		
		public List<RoomType> GetAll()
		{
			return _roomTypeDAO.GetAll();
		}

		public RoomType GetByName(string name)
		{
			return (from roomType in _roomTypeDAO.GetAll()
					where roomType.RoomTypeName.Equals(name)
					select roomType).FirstOrDefault();
		}
	}
}
