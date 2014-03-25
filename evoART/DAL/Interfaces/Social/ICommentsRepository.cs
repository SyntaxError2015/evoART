﻿using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Social
{
    interface ICommentsRepository
    {
        SocialModels.Comment[] GetCommentsForPhoto(Guid photoId);

        SocialModels.Comment[] GetCommentsForPhoto(PhotoModels.Photo photo);

        bool Insert(Guid photoId, Guid userId);

        bool Insert(Guid photoId, AccountModels.UserAccount user);

        bool Insert(Guid photoId, string userName);

        bool Insert(PhotoModels.Photo photo, Guid userId);

        bool Insert(PhotoModels.Photo photo, AccountModels.UserAccount user);

        bool Insert(PhotoModels.Photo photo, string userName);

        bool Delete(Guid photoId, Guid userId);

        bool Delete(Guid photoId, AccountModels.UserAccount user);

        bool Delete(Guid photoId, string userName);

        bool Delete(PhotoModels.Photo photo, Guid userId);

        bool Delete(PhotoModels.Photo photo, AccountModels.UserAccount user);

        bool Delete(PhotoModels.Photo photo, string userName);

        bool Delete(SocialModels.Comment comment);

        bool Update(SocialModels.Comment comment);
    }
}
