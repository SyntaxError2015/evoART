using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IHashTagsRepository
    {
        PhotoModels.HashTag[] GetPopularHashTags(int number, string name = "");

        bool DeleteHashTagForPhoto(PhotoModels.HashTag hashTag, PhotoModels.Photo photo);

        bool DeleteHashTagForPhoto(Guid hashTagId, PhotoModels.Photo photo);

        bool DeleteHashTagForPhoto(Guid hashTagId, Guid keywordId);

        bool DeleteAllHasheTagsForPhoto(Guid photoId);

        bool Insert(string[] hashTagNames, Guid photoId);

        bool DeleteHashTag(Guid hashTagId);

        bool DeleteHashTag(string hashTagName);

    }
}
