using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IPhotosRepository
    {
        bool VerifyExists(AccountModels.UserAccount user, PhotoModels.Album album, string photoName);

        bool Insert(PhotoModels.Photo photo);

        bool Insert(string name, string description);

        bool Delete(AccountModels.UserAccount user, PhotoModels.Album album, string photoName);

        bool Update(PhotoModels.Photo photo);
    }
}
