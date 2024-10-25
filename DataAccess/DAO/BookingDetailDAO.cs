using BusinessObjects;

namespace DataAccess.DAO
{
    public class BookingDetailDAO(AppDbContext appDbContext) : IDataAccessObject<BookingDetail>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public void Add(BookingDetail entity)
        {
            _appDbContext.BookingDetails.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void AddRange(List<BookingDetail> bookingDetailList)
        {
            _appDbContext.BookingDetails.AddRange(bookingDetailList);
            _appDbContext.SaveChanges();
        }

        public void Delete(BookingDetail entity)
        {
            _appDbContext.BookingDetails.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<BookingDetail> GetAll()
        {
            return _appDbContext.BookingDetails;
        }

        public void Update(BookingDetail entity)
        {
            _appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContext.SaveChanges();
        }
    }
}
