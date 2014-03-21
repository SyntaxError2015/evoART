using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IAlbumsRepository
    {
        bool Insert(PhotoModels.Album album);

        bool Insert(string albumName, string albumDescription);

        bool Delete(string albumName);

        bool Update(PhotoModels.Album album);
    }
}
