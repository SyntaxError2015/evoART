using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IPhotosRepository
    {
        PhotoModels.Photo GetPhoto(int photoId);

        bool VerifyExists(Guid userId, Guid albumId, string photoName);

        // Returns the photo Id
        Guid Insert(PhotoModels.Photo photo);

        // Returns the photo Id
        Guid Insert(string photoName, string photoDescription, Guid albumId, Guid userId);

        bool Delete(Guid photoId);

        bool Update(PhotoModels.Photo photo);
    }
}
