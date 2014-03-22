using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IPhotosRepository
    {
        PhotoModels.Photo GetPhoto(int photoId);

        bool VerifyExists(int userId, int albumId, string photoName);

        // Returns the photo Id
        int Insert(PhotoModels.Photo photo);

        // Returns the photo Id
        int Insert(string photoName, string photoDescription, int albumId, int userId);

        bool Delete(int photoId);

        bool Update(PhotoModels.Photo photo);
    }
}
