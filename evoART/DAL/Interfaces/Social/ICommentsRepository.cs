using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Social
{
    interface ICommentsRepository
    {
        SocialModels.Comment GetComment(Guid commentId);

        SocialModels.Comment[] GetCommentsForPhoto(Guid photoId);

        bool Insert(Guid photoId, Guid userId, string commentText);

        bool Insert(Guid photoId, AccountModels.UserAccount user, string commentText);

        bool Insert(Guid photoId, string userName, string commentText);

        bool Insert(PhotoModels.Photo photo, Guid userId, string commentText);

        bool Insert(PhotoModels.Photo photo, AccountModels.UserAccount user, string commentText);

        bool Insert(PhotoModels.Photo photo, string userName, string commentText);

        bool Insert(SocialModels.Comment comment);

        bool Delete(Guid commentId);

        bool Update(SocialModels.Comment comment);
    }
}
