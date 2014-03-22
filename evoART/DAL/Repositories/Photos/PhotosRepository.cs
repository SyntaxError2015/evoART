using System;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class PhotosRepository : IPhotosRepository
    {
        public PhotoModels.Photo GetPhoto(int photoId)
        {
            throw new NotImplementedException();
        }

        public bool VerifyExists(int userId, int albumId, string photoName)
        {
            throw new NotImplementedException();
        }

        public int Insert(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }

        public int Insert(string photoName, string photoDescription, int albumId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int photoId)
        {
            throw new NotImplementedException();
        }

        public bool Update(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}