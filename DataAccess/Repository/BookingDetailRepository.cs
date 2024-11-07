using BusinessObjects;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

namespace DataAccess.Repository
{
    public class BookingDetailRepository(BookingDetailDAO bookingDetailDAO) : IBookingDetailRepository
    {
        private readonly BookingDetailDAO _bookingDetailDAO = bookingDetailDAO;

        public void Add(BookingDetail entity)
        {
            _bookingDetailDAO.Add(entity);
        }

        public void AddRange(List<BookingDetail> bookingDetailList)
        {
            _bookingDetailDAO.AddRange(bookingDetailList);
        }

        public void Delete(BookingDetail entity)
        {
            _bookingDetailDAO.Delete(entity);
        }

        public IEnumerable<BookingDetail> GetAll()
        {
            return _bookingDetailDAO.GetAll();
        }

        public IEnumerable<BookingDetail> GetByBookingReservationId(int id)
        {
            return (from bookingDetail in _bookingDetailDAO.GetAll()
                    where bookingDetail.BookingReservationId == id
                    select bookingDetail);
        }

        public void Update(BookingDetail entity)
        {
            _bookingDetailDAO.Update(entity);
        }
    }
}
