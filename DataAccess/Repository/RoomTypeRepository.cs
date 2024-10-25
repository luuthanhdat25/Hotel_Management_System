using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class RoomTypeRepository(RoomTypeDAO roomTypeDAO) : IRoomTypeRepository
	{
		private readonly RoomTypeDAO _roomTypeDAO = roomTypeDAO;

        public void Add(RoomType entity)
        {
            _roomTypeDAO.Add(entity);
        }

        public void Delete(RoomType entity)
        {
            _roomTypeDAO.Delete(entity);
        }

        public IEnumerable<RoomType> GetAll()
        {
            return _roomTypeDAO.GetAll();
        }

        public void Update(RoomType entity)
        {
            _roomTypeDAO.Update(entity);
        }
    }
}
