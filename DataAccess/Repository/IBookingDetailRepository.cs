using BusinessObjects;

namespace DataAccess.Repository
{
    public interface IBookingDetailRepository
    {
        public List<BookingDetail> GetAll();
        public List<BookingDetail> GetByBookingReservationId(int id);
        public void Add(BookingDetail entity);
        public void AddRange(List<BookingDetail> bookingDetailList);
        public void Delete(BookingDetail entity);
    }
}
