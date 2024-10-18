using BusinessObject;

namespace DataAccess.Repository
{
    public interface IBookingReservationRepository
    {
        public List<BookingReservation> GetAll();
        public void Add(BookingReservation entity);
        public void Delete(BookingReservation entity);
        public BookingReservation GetById(int id);
        public List<BookingReservation> GetByCustomerId(int id);
    }
}
