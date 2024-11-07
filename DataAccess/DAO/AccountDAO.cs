using BusinessObject;

namespace DataAccess.DAO
{
    public class AccountDAO(AppDbContext appDbContext) : IDataAccessObject<Account>
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        public void Add(Account entity)
        {
            _appDbContext.Accounts.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Delete(Account entity)
        {
            _appDbContext.Accounts.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Account> GetAll()
        {
            return _appDbContext.Accounts;
        }

        public void Update(Account entity)
        {
            _appDbContext.Update(entity);
        }
    }
}
