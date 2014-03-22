using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IHashTagsRepository
    {
        PhotoModels.HashTag[] GetPopularHashTags(int number, string name = "");

        bool Insert(string[] hashTagName, Guid photoId);

        bool DeleteHashForPhoto(Guid photoId, Guid keywordId);

        bool DeleteAllHashesForPhoto(Guid photoId);
    }
}
