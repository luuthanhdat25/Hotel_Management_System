using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repository.Interface;

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

        public IEnumerable<BookingReservation> GetAll()
        {
            return _bookingReservationDAO.GetAll();
        }

        public IEnumerable<BookingReservation> GetByCustomerId(int id)
        {
            return (from bookingReservation in _bookingReservationDAO.GetAll()
                    where bookingReservation.CustomerId == id
                    select bookingReservation);
        }

        public BookingReservation? GetById(int id)
        {
            return _bookingReservationDAO.GetAll()
                .FirstOrDefault(bookRe => bookRe.BookingReservationId == id);
        }

        public void Update(BookingReservation entity)
        {
            _bookingReservationDAO.Update(entity);
        }
    }
}
