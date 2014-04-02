using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IPhotosRepository
    {
        PhotoModels.Photo GetPhoto(Guid photoId);

        PhotoModels.Photo GetPhoto(Guid albumId, string photoName);

        PhotoModels.Photo[] GetPopularPhotos(int startPosition, int number);

        PhotoModels.Photo[] GetPopularPhotosForUser(Guid userId, int startPosition, int number);

        PhotoModels.Photo[] GetPopularPhotosForUser(string userName, int startPosition, int number);

        PhotoModels.Photo[] GetPhotosForHashTag(PhotoModels.HashTag hashTag, int startPosition, int number);

        PhotoModels.Photo[] GetPhotosForHashTag(string hashTagName, int startPosition, int number);

        bool IncrementViews(Guid photoId);

        bool VerifyExists(Guid photoId);

        PhotoModels.Photo[] GetPhotosFromAlbum(Guid albumId);

        // Returns the photo Id
        Guid Insert(PhotoModels.Photo photo);

        // Returns the photo Id
        Guid Insert(string photoName, string photoDescription, PhotoModels.Album album, PhotoModels.ContentTag contentTag);

        bool Delete(Guid photoId);

        bool Update(PhotoModels.Photo photo);
    }
}
