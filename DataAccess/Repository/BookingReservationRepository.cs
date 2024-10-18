using BusinessObject;
using DataAccess.DAO;

namespace DataAccess.Repository
{
    public class BookingReservationRepository(BookingReservationDAO bookingReservationDAO) : IBookingReservationRepository
    {
        private readonly BookingReservationDAO _bookingReservationDAO = bookingReservationDAO;

        public void Add(BookingReservation entity)
        {
            _bookingReservationDAO.Add(entity);
        }

        public void Delete(BookingReservation entity)
        {
            _bookingReservationDAO.Delete(entity);
        }

        public List<BookingReservation> GetAll()
        {
            return _bookingReservationDAO.GetAll();
        }

        public List<BookingReservation> GetByCustomerId(int id)
        {
            return (from bookingReservation in _bookingReservationDAO.GetAll()
                    where bookingReservation.CustomerId == id
                    select bookingReservation).ToList();
        }

        public BookingReservation GetById(int id)
        {
            return _bookingReservationDAO.GetById(id);
        }
    }
}
