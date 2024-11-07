using BusinessObject;

namespace DataAccess.Repository.Interface
{
    public interface IBookingReservationRepository : IRepository<BookingReservation>
    {
        public BookingReservation? GetById(int id);
        public IEnumerable<BookingReservation> GetAllByCustomerId(int id);
    }
}
