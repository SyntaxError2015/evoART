using System.Data.Entity;
using evoART.DAL.DbContexts;
using evoART.DAL.Repositories;
using evoART.Models;

namespace evoART.DAL.UnitOfWork
{
    /// <summary>
    /// The entire Unit Of Work for the database.
    /// 
    /// Thsi class is a singleton
    /// </summary>
    public class DatabaseWorkUnit : IDatabaseWorkUnit
    {
        private DatabaseWorkUnit()
        {

        }

        // All the fields here are singletons and their values are instantiated in the representing properties
        #region fields
        private static DatabaseWorkUnit _instance = null;

        private UserAccountRepository _userAccountRepository = null;
        private AccountValidationRepository _accountValidationRepository = null;
        private SessionRepository _sessionRepository = null;
        private RoleRepository _roleRepository = null;

        #endregion

        // Singleton initializations
        #region Properties

        /// <summary>
        /// Get the available instance for this class
        /// </summary>
        public static DatabaseWorkUnit Instance
        {
            get
            {
                return _instance ?? (_instance = new DatabaseWorkUnit());
            }
        }

        /// <summary>
        /// Get the repository representing the UserAccount model
        /// </summary>
        public UserAccountRepository UserAccountRepository
        {
            get
            {
                return _userAccountRepository ?? (_userAccountRepository = new UserAccountRepository(new DatabaseContext()));
            }
        }

        /// <summary>
        /// Get the repository representing the AccountValidation model
        /// </summary>
        public AccountValidationRepository AccountValidationRepository
        {
            get
            {
                return _accountValidationRepository ?? (_accountValidationRepository = new AccountValidationRepository(new DatabaseContext()));
            }
        }

        /// <summary>
        /// Get the repository representing the Session model
        /// </summary>
        public SessionRepository SessionRepository
        {
            get
            {
                return _sessionRepository ?? (_sessionRepository = new SessionRepository(new DatabaseContext()));
            }
        }

        /// <summary>
        /// Get the repository representing the Role model
        /// </summary>
        public RoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ?? (_roleRepository = new RoleRepository(new DatabaseContext()));
            }
        }

        #endregion

    }
}