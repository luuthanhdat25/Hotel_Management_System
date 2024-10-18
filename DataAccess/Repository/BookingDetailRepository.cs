using BusinessObjects;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class BookingDetailRepository(BookingDetailDAO bookingDetailDAO) : IBookingDetailRepository
    {
        private readonly BookingDetailDAO bookingDetailDAO = bookingDetailDAO;

        public void Add(BookingDetail entity)
        {
            bookingDetailDAO.Add(entity);
        }

        public void AddRange(List<BookingDetail> bookingDetailList)
        {
            bookingDetailDAO.AddRange(bookingDetailList);
        }

        public void Delete(BookingDetail entity)
        {
            bookingDetailDAO.Delete(entity);
        }

        public List<BookingDetail> GetAll()
        {
            return bookingDetailDAO.GetAll();
        }

        public List<BookingDetail> GetByBookingReservationId(int id)
        {
            return (from bookingDetail in bookingDetailDAO.GetAll()
                    where bookingDetail.BookingReservationId == id
                    select bookingDetail).ToList();
        }
    }
}
