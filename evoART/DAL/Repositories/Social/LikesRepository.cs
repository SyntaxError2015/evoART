using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Social;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Social
{
    public class LikesRepository : BaseRepository<SocialModels.Like>, ILikesRepository
    {
        public LikesRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Get the number of likes that a photo has
        /// </summary>
        /// <param name="photoId">The Id of the photo for which to count the likes</param>
        /// <returns>An integer value</returns>
        public int GetNumberOfLikes(Guid photoId)
        {
            try
            {
                return _dbSet.Count(p => p.Photo.PhotoId == photoId);
            }

            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Get the number of likes that a photo has
        /// </summary>
        /// <param name="photo">The Photo entity for which to count the likes</param>
        /// <returns>An integer value</returns>
        public int GetNumberOfLikes(PhotoModels.Photo photo)
        {
            try
            {
                return _dbSet.Count(p => p.Photo == photo);
            }

            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Get all the users that have liked a certain photo
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <returns>An UserAccount entity array</returns>
        public AccountModels.UserAccount[] GetUsersWhoLikedPhoto(Guid photoId)
        {
            try
            {
                IEnumerable<SocialModels.Like> likes = _dbSet.Where(p => p.Photo.PhotoId == photoId);

                var users = new AccountModels.UserAccount[likes.Count()];
                for (var i = 0; i < users.Length; i++)
                    users[i] = likes.ElementAt(i).UserAccount;

                return users;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get all the users that have liked a certain photo
        /// </summary>
        /// <param name="photo">The Photo entity</param>
        /// <returns>An UserAccount entity array</returns>
        public AccountModels.UserAccount[] GetUsersWhoLikedPhoto(PhotoModels.Photo photo)
        {
            try
            {
                IEnumerable<SocialModels.Like> likes = _dbSet.Where(p => p.Photo == photo);

                var users = new AccountModels.UserAccount[likes.Count()];
                for (var i = 0; i < users.Length; i++)
                    users[i] = likes.ElementAt(i).UserAccount;

                return users;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Verify if a certain user has already liked a photo or not
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="photoId">The Id of the photo</param>
        public bool UserHasLikedPhoto(Guid userId, Guid photoId)
        {
            try
            {
                return _dbSet.Count(l => l.UserAccount.UserId == userId && l.Photo.PhotoId == photoId) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify if a certain user has already liked a photo or not
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="photo">The Photo entity representing the photo</param>
        public bool UserHasLikedPhoto(Guid userId, PhotoModels.Photo photo)
        {
            try
            {
                return _dbSet.Count(l => l.UserAccount.UserId == userId && l.Photo == photo) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify if a certain user has already liked a photo or not
        /// </summary>
        /// <param name="user">The UserAccount entity representing the user</param>
        /// <param name="photoId">The Id of the photo</param>
        public bool UserHasLikedPhoto(AccountModels.UserAccount user, Guid photoId)
        {
            try
            {
                return _dbSet.Count(l => l.UserAccount == user && l.Photo.PhotoId == photoId) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Verify if a certain user has already liked a photo or not
        /// </summary>
        /// <param name="user">The UserAccount entity representing the user</param>
        /// <param name="photo">The Photo entity representing the photo</param>
        public bool UserHasLikedPhoto(AccountModels.UserAccount user, PhotoModels.Photo photo)
        {
            try
            {
                return _dbSet.Count(l => l.UserAccount == user && l.Photo == photo) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new Like to the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="userId">The Id of the user that has liked the photo</param>
        public bool Insert(Guid photoId, Guid userId)
        {
            try
            {
                var like = new SocialModels.Like
                {
                    LikeId = Guid.NewGuid(),
                    LikeDate = DateTime.Now,
                    Photo = _dbContext.Photos.Find(photoId),
                    UserAccount = _dbContext.UserAccounts.Find(userId)
                };

                _dbSet.Add(like);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new Like to the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="user">The UserAccount entity representing the user that liked the photo</param>
        public bool Insert(Guid photoId, AccountModels.UserAccount user)
        {
            try
            {
                var like = new SocialModels.Like
                {
                    LikeId = Guid.NewGuid(),
                    LikeDate = DateTime.Now,
                    Photo = _dbContext.Photos.Find(photoId),
                    UserAccount = user
                };

                _dbSet.Add(like);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new Like to the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="userName">The nickname of the user that has liked the photo</param>
        public bool Insert(Guid photoId, string userName)
        {
            try
            {
                var like = new SocialModels.Like
                {
                    LikeId = Guid.NewGuid(),
                    LikeDate = DateTime.Now,
                    Photo = _dbContext.Photos.Find(photoId),
                    UserAccount = _dbContext.UserAccounts.First(u => u.UserName == userName)
                };

                _dbSet.Add(like);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new Like to the database
        /// </summary>
        /// <param name="photo">A Photo entity representing the photo</param>
        /// <param name="userId">The Id of the user that has liked the photo</param>
        public bool Insert(PhotoModels.Photo photo, Guid userId)
        {
            try
            {
                var like = new SocialModels.Like
                {
                    LikeId = Guid.NewGuid(),
                    LikeDate = DateTime.Now,
                    Photo = photo,
                    UserAccount = _dbContext.UserAccounts.Find(userId)
                };

                _dbSet.Add(like);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new Like to the database
        /// </summary>
        /// <param name="photo">A Photo entity representing the photo</param>
        /// <param name="user">A UserAccount entity representing the user that liked the photo</param>
        public bool Insert(PhotoModels.Photo photo, AccountModels.UserAccount user)
        {
            try
            {
                var like = new SocialModels.Like
                {
                    LikeId = Guid.NewGuid(),
                    LikeDate = DateTime.Now,
                    Photo = photo,
                    UserAccount = user
                };

                _dbSet.Add(like);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new Like to the database
        /// </summary>
        /// <param name="photo">A Photo entity representing the photo</param>
        /// <param name="userName">The nickname of the user that liked the photo</param>
        public bool Insert(PhotoModels.Photo photo, string userName)
        {
            try
            {
                var like = new SocialModels.Like
                {
                    LikeId = Guid.NewGuid(),
                    LikeDate = DateTime.Now,
                    Photo = photo,
                    UserAccount = _dbContext.UserAccounts.First(u => u.UserName == userName)
                };

                _dbSet.Add(like);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a Like from the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="userId">The Id of the user that has liked the photo</param>
        public bool Delete(Guid photoId, Guid userId)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(l => l.Photo.PhotoId == photoId && l.UserAccount.UserId == userId));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a Like from the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="user">The UserAccount entity representing the user that has liked the photo</param>
        public bool Delete(Guid photoId, AccountModels.UserAccount user)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(l => l.Photo.PhotoId == photoId && l.UserAccount == user));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a Like from the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="userName">The nickname of the user that has liked the photo</param>
        public bool Delete(Guid photoId, string userName)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(l => l.Photo.PhotoId == photoId && l.UserAccount.UserName == userName));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a Like from the database
        /// </summary>
        /// <param name="photo">The Photo entity representing the photo</param>
        /// <param name="userId">The Id of the user that has liked the photo</param>
        public bool Delete(PhotoModels.Photo photo, Guid userId)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(l => l.Photo == photo && l.UserAccount.UserId == userId));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a Like from the database
        /// </summary>
        /// <param name="photo">The Photo entity representing the photo</param>
        /// <param name="user">The UserAccount entity representing the user that has liked the photo</param>
        public bool Delete(PhotoModels.Photo photo, AccountModels.UserAccount user)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(l => l.Photo == photo && l.UserAccount == user));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a Like from the database
        /// </summary>
        /// <param name="photo">The Photo entity representing the photo</param>
        /// <param name="userName">The nickname of the user that has liked the photo</param>
        public bool Delete(PhotoModels.Photo photo, string userName)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(l => l.Photo == photo && l.UserAccount.UserName == userName));

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}