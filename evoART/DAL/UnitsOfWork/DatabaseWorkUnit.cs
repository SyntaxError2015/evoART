using System;
using evoART.DAL.DbContexts;
using evoART.DAL.Repositories.Photos;
using evoART.DAL.Repositories.Social;
using evoART.DAL.Repositories.UserAccounts;
using evoART.Models.DbModels;

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
        private HashTagsRepository _hashTagsRepository;
        private ContentTagsRepository _contentTagsRepository;

        #endregion

        #region Fields for the Social module

        private LikesRepository _likesRepository;
        private CommentsRepository _commentsRepository;

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

        /// <summary>
        /// Get the repository representing the Photos model
        /// </summary>
        public PhotosRepository PhotosRepository
        {
            get
            {
                return _photosRepository ?? (_photosRepository = new PhotosRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the Albums model
        /// </summary>
        public AlbumsRepository AlbumsRepository
        {
            get
            {
                return _albumsRepository ?? (_albumsRepository = new AlbumsRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the HashTags model
        /// </summary>
        public HashTagsRepository HashTagsRepository
        {
            get
            {
                return _hashTagsRepository ?? (_hashTagsRepository = new HashTagsRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the ContentTags model
        /// </summary>
        public ContentTagsRepository ContentTagsRepository
        {
            get
            {
                return _contentTagsRepository ?? (_contentTagsRepository = new ContentTagsRepository(_dbContext));
            }
        }

        #endregion

        #region Properties for the Social module

        /// <summary>
        /// Get the repository representing the Likes model
        /// </summary>
        public LikesRepository LikesRepository
        {
            get
            {
                return _likesRepository ?? (_likesRepository = new LikesRepository(_dbContext));
            }
        }

        /// <summary>
        /// Get the repository representing the Comments repository
        /// </summary>
        public CommentsRepository CommentsRepository
        {
            get
            {
                return _commentsRepository ?? (_commentsRepository = new CommentsRepository(_dbContext));
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