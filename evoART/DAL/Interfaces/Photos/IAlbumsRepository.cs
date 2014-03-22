﻿using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IAlbumsRepository
    {
        PhotoModels.Album[] GetAlbumsForUser(Guid userId);

        bool Insert(PhotoModels.Album album);

        bool Insert(string albumName, string albumDescription = "");

        bool Delete(string albumName, Guid userId);

        bool Update(PhotoModels.Album album);
    }
}
