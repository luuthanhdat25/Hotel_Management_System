using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class BookingReservationDAO(AppDbContext appDbContext) : IDataAccessObject<BookingReservation>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public void Add(BookingReservation entity)
        {
            _appDbContext.BookingReservations.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Delete(BookingReservation entity)
        {
            _appDbContext.BookingReservations.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<BookingReservation> GetAll()
        {
            return _appDbContext.BookingReservations
                .Include(br => br.Customer) 
                .ToList();
        }

        public void Update(BookingReservation entity)
        {
            _appDbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContext.SaveChanges();
        }
    }
}
