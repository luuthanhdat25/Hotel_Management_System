using BusinessObject;

namespace DataAccess.Repository.Interface
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account? GetByEmail(string email);
    }
}
