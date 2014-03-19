using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;

namespace evoART.DAL.Repositories
{
    public abstract class BaseRepository : IBaseInterface
    {
        // ReSharper disable once InconsistentNaming
        internal readonly DatabaseContext _dbContext;

        internal BaseRepository(DatabaseContext context)
        {
            _dbContext = context;
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