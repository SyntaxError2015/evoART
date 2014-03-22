﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class AlbumsRepository : BaseRepository<PhotoModels.Album>, IAlbumsRepository
    {
        public AlbumsRepository(DatabaseContext context) : base(context)
        {
        }

        public PhotoModels.Album[] GetAlbumsForUser(Guid userId)
        {
            throw new NotImplementedException();
            //IEnumerable<PhotoModels.Album> albums = _dbSet.Where(a => a.Photos.Contains())
        }

        public PhotoModels.Photo[] GetAlbumPhotos(Guid albumId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PhotoModels.Album album)
        {
            throw new NotImplementedException();
        }

        public bool Insert(string albumName, string albumDescription)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string albumName)
        {
            throw new NotImplementedException();
        }

        public bool Update(PhotoModels.Album album)
        {
            throw new NotImplementedException();
        }
    }
}