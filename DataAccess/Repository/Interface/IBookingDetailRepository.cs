using BusinessObjects;

namespace DataAccess.Repository.Interface
{
    public interface IBookingDetailRepository : IRepository<BookingDetail>
    {
        public IEnumerable<BookingDetail> GetByBookingReservationId(int id);
        public void AddRange(List<BookingDetail> bookingDetailList);
    }
}
