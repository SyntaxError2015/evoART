using System;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Social;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Social
{
    public class CommentsRepository : BaseRepository<SocialModels.Comment>, ICommentsRepository
    {
        public CommentsRepository(DatabaseContext context) : base(context)
        {
        }

        public SocialModels.Comment[] GetCommentsForPhoto(Guid photoId)
        {
            throw new NotImplementedException();
        }

        public SocialModels.Comment[] GetCommentsForPhoto(PhotoModels.Photo photo)
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

        public bool Delete(SocialModels.Comment comment)
        {
            throw new NotImplementedException();
        }

        public bool Update(SocialModels.Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}