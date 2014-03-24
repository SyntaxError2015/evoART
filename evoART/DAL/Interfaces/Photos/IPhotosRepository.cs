using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IPhotosRepository
    {
        PhotoModels.Photo GetPhoto(Guid photoId);

        PhotoModels.Photo GetPhoto(Guid albumId, string photoName);

        PhotoModels.Photo[] GetPhotosForHashTag(PhotoModels.HashTag hashTag, int startPosition, int number);

        PhotoModels.Photo[] GetPhotosForHashTag(string hashTagName, int startPosition, int number);

        bool VerifyExists(Guid albumId, string photoName);

        PhotoModels.Photo[] GetPhotosFromAlbum(Guid albumId);

        // Returns the photo Id
        Guid Insert(PhotoModels.Photo photo);

        // Returns the photo Id
        Guid Insert(string photoName, string photoDescription, PhotoModels.Album album);

        bool Delete(Guid photoId);

        bool Update(PhotoModels.Photo photo);
    }
}
