using System;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.DAL.Repositories.UserAccounts;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class AlbumsRepository : BaseRepository<PhotoModels.Album>, IAlbumsRepository
    {
        public AlbumsRepository(DatabaseContext context) : base(context)
        {
        }

        public PhotoModels.Album[] GetAlbumsForUser(int userId)
        {
            throw new NotImplementedException();
        }

        public PhotoModels.Photo[] GetAlbumPhotos(int albumId, int userId)
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