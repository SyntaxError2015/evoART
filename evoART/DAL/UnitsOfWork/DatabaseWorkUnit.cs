using System;
using evoART.DAL.DbContexts;
using evoART.DAL.Repositories.Photos;
using evoART.DAL.Repositories.UserAccounts;

namespace evoART.DAL.UnitsOfWork
{
    /// <summary>
    /// The entire Unit Of Work for the database.
    /// 
    /// Thsi class is a singleton
    /// </summary>
    public class DatabaseWorkUnit : IDisposable
    {
        private DatabaseWorkUnit()
        {
            _dbContext = new DatabaseContext();
        }
        
        private static DatabaseWorkUnit _instance;
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Get the available instance for this class
        /// </summary>
        public static DatabaseWorkUnit Instance
        {
            get
            {
                _disposed = false;
                return _instance ?? (_instance = new DatabaseWorkUnit());
            }
        }

        // All the fields here are singletons and their values are instantiated in the representing properties
        #region Fields for the User Accounts module

        private UserAccountRepository _userAccountRepository;
        private AccountValidationRepository _accountValidationRepository;
        private SessionRepository _sessionRepository;
        private RoleRepository _roleRepository;
        private OAuthLoginRepository _oAuthLoginRepository;

        #endregion

        #region Fields for the Photos module

        private PhotosRepository _photosRepository;
        private AlbumsRepository _albumsRepository;
        private CategoriesRepository _categoriesRepository;
        private KeywordsRepository _keywordsRepository;

        #endregion

        // Singleton initializations
        #region Properties for the User Accounts module

        /// <summary>
        /// Get the repository representing the UserAccount model
        /// </summary>
        public UserAccountRepository UserAccountRepository
        {
            get
            {
                return _userAccountRepository ?? (_userAccountRepository = new UserAccountRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the AccountValidation model
        /// </summary>
        public AccountValidationRepository AccountValidationRepository
        {
            get
            {
                return _accountValidationRepository ?? (_accountValidationRepository = new AccountValidationRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the Session model
        /// </summary>
        public SessionRepository SessionRepository
        {
            get
            {
                return _sessionRepository ?? (_sessionRepository = new SessionRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the Role model
        /// </summary>
        public RoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ?? (_roleRepository = new RoleRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the OAuthLoginModel
        /// </summary>
        public OAuthLoginRepository OAuthLoginRepository
        {
            get
            {
                return _oAuthLoginRepository ?? (_oAuthLoginRepository = new OAuthLoginRepository(_dbContext));
            }
        }

        #endregion

        #region Properties for the Photos module

        public PhotosRepository PhotosRepository
        {
            get
            {
                return _photosRepository ?? (_photosRepository = new PhotosRepository());
            }
        }

        public AlbumsRepository AlbumsRepository
        {
            get
            {
                return _albumsRepository ?? (_albumsRepository = new AlbumsRepository());
            }
        }

        public CategoriesRepository CategoriesRepository
        {
            get
            {
                return _categoriesRepository ?? (_categoriesRepository = new CategoriesRepository());
            }
        }

        public KeywordsRepository KeywordsRepository
        {
            get
            {
                return _keywordsRepository ?? (_keywordsRepository = new KeywordsRepository());
            }
        }

        #endregion

        #region Disposing logic

        private static bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;

            _dbContext.Dispose();
            _disposed = true;
        }

        #endregion Disposing logic
    }
}