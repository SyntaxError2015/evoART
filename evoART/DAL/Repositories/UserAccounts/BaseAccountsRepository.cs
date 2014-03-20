using System.Data.Entity;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.UserAccounts;

namespace evoART.DAL.Repositories.UserAccounts
{
    public abstract class BaseRepository<T> : IBaseInterface where T : class
    {
        internal readonly UserAccountsContext _dbContext;
        internal readonly DbSet<T> _dbSet;

        internal BaseRepository(UserAccountsContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        #region Persistence logic

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool SaveAsync()
        {
            try
            {
                _dbContext.SaveChangesAsync();

                return true;
            }

            catch
            {
                return false;
            }
        }

        #endregion
    }
}