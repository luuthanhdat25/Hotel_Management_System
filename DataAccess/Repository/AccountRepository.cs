using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repository.Interface;


namespace DataAccess.Repository
{
    public class AccountRepository(AccountDAO accountDAO) : IAccountRepository
    {
        private readonly AccountDAO _accountDAO = accountDAO;

        public void Add(Account entity)
        {
            _accountDAO.Add(entity);
        }

        public void Delete(Account entity)
        {
            _accountDAO.Delete(entity);
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountDAO.GetAll();
        }

        public Account? GetByEmail(string email)
        {
            return _accountDAO.GetAll()
                .FirstOrDefault(account => account.EmailAddress.ToLower().Equals(email.ToLower()));
        }

        public void Update(Account entity)
        {
            _accountDAO.Update(entity);
        }
    }
}
