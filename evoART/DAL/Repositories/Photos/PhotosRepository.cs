using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class PhotosRepository : IPhotosRepository
    {
        public bool VerifyExists(AccountModels.UserAccount user, PhotoModels.Album album, string photoName)
        {
            throw new NotImplementedException();
        }

        public bool Insert(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }

        public bool Insert(string name, string description)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AccountModels.UserAccount user, PhotoModels.Album album, string photoName)
        {
            throw new NotImplementedException();
        }

        public bool Update(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}