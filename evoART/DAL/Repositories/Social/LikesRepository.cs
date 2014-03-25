using System;
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

        public int GetNumberOfLikes(Guid photoId)
        {
            throw new NotImplementedException();
        }

        public int GetNumberOfLikes(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }

        public AccountModels.UserAccount[] GetUsersWhoLikedPhoto(Guid photoId)
        {
            throw new NotImplementedException();
        }

        public AccountModels.UserAccount[] GetUsersWhoLikedPhoto(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }

        public bool UserHasLikedPhoto(Guid userId, Guid photoId)
        {
            throw new NotImplementedException();
        }

        public bool UserHasLikedPhoto(Guid userId, PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }

        public bool UserHasLikedPhoto(AccountModels.UserAccount user, Guid photoId)
        {
            throw new NotImplementedException();
        }

        public bool UserHasLikedPhoto(AccountModels.UserAccount user, PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Guid photoId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Guid photoId, AccountModels.UserAccount user)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Guid photoId, string userName)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PhotoModels.Photo photo, Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PhotoModels.Photo photo, AccountModels.UserAccount user)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PhotoModels.Photo photo, string userName)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid photoId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid photoId, AccountModels.UserAccount user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid photoId, string userName)
        {
            throw new NotImplementedException();
        }

        public bool Delete(PhotoModels.Photo photo, Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(PhotoModels.Photo photo, AccountModels.UserAccount user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(PhotoModels.Photo photo, string userName)
        {
            throw new NotImplementedException();
        }
    }
}