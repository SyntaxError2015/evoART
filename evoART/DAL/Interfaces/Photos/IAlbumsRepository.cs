using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IAlbumsRepository
    {
        PhotoModels.Album[] GetAlbumsForUser(int userId);

        PhotoModels.Photo[] GetAlbumPhotos(int albumId, int userId);

        bool Insert(PhotoModels.Album album);

        bool Insert(string albumName, string albumDescription);

        bool Delete(string albumName);

        bool Update(PhotoModels.Album album);
    }
}
