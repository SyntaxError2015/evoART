using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IHashTagsRepository
    {
        PhotoModels.HashTag[] GetPopularHashTags(int number, string name = "");

        bool Insert(string[] hashTagName, int photoId);

        bool DeleteHashForPhoto(int photoId, int keywordId);

        bool DeleteAllHashesForPhoto(int photoId);
    }
}
