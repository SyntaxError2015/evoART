﻿using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IAlbumsRepository
    {
        PhotoModels.Album GetAlbum(Guid albumId);

        PhotoModels.Album[] GetAlbumsForUser(Guid userId);

        bool Insert(PhotoModels.Album album, Guid userId);

        bool Insert(Guid userId, string albumName, string albumDescription = "");

        bool Delete(Guid albumId, Guid userId);

        bool Update(PhotoModels.Album album);
    }
}
