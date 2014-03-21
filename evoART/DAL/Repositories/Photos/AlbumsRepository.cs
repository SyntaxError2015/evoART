using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class AlbumsRepository : IAlbumsRepository
    {
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