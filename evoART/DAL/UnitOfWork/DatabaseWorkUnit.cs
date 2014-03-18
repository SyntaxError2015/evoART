using evoART.DAL.DbContexts;
using evoART.DAL.Repositories;

namespace evoART.DAL.UnitOfWork
{
    public class DatabaseWorkUnit
    {
        private DatabaseWorkUnit()
        {
            
        }

        /// <summary>
        /// All the fields here are singletons and their values are instantiated in the representing properties
        /// </summary>

        #region fields
        private static DatabaseWorkUnit _instance = null;

        private UserAccountRepository _userAccountRepository = null;
        private AccountValidationRepository _accountValidationRepository = null;
        private SessionRepository _sessionRepository = null;
        private RoleRepository _roleRepository = null;

        #endregion

        /// <summary>
        /// Singleton initializations
        /// </summary>

        #region properties

        public static DatabaseWorkUnit Instance
        {
            get
            {
                return _instance ?? (_instance = new DatabaseWorkUnit());
            }
        }

        public UserAccountRepository UserAccountRepository
        {
            get
            {
                return _userAccountRepository ?? (_userAccountRepository = new UserAccountRepository(DatabaseContext.Instance));
            }
        }

        public AccountValidationRepository AccountValidationRepository
        {
            get
            {
                return _accountValidationRepository ?? (_accountValidationRepository = new AccountValidationRepository(DatabaseContext.Instance));
            }
        }

        public SessionRepository SessionRepository
        {
            get
            {
                return _sessionRepository ?? (_sessionRepository = new SessionRepository(DatabaseContext.Instance));
            }
        }

        public RoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ?? (_roleRepository = new RoleRepository(DatabaseContext.Instance));
            }
        }

        #endregion

    }
}