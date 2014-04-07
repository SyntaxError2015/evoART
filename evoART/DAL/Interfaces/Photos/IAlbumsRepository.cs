using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IAlbumsRepository
    {
        PhotoModels.Album GetAlbum(Guid albumId);

        PhotoModels.Album[] GetAlbumsForUser(Guid userId, Guid guestUserId);

        bool VerifyExists(Guid albumId);

        bool Insert(PhotoModels.Album album, Guid userId);

        bool Insert(Guid userId, string albumName, bool isPrivate = false, string albumDescription = "");

        bool Delete(Guid albumId);

        bool Update(PhotoModels.Album album);
    }
}
