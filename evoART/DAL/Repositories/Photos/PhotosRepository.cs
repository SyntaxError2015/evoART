using System;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.DAL.Repositories.UserAccounts;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class PhotosRepository : BaseRepository<PhotoModels.Photo>, IPhotosRepository
    {
        public PhotosRepository(DatabaseContext context) : base(context)
        {
        }

        public PhotoModels.Photo GetPhoto(int photoId)
        {
            try
            {
                return _dbSet.Find(photoId);
            }

            catch
            {
                return null;
            }
        }

        public bool VerifyExists(int userId, int albumId, string photoName)
        {
            try
            {
                return _dbSet.Count(p =>
                    p.UserAccount.UserId == userId &&
                    p.Album.AlbumId == albumId &&
                    p.Name == photoName) > 0;
            }

            catch
            {
                return false;
            }
        }

        public int Insert(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }

        public int Insert(string photoName, string photoDescription, int albumId, int userId)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int photoId)
        {
            throw new NotImplementedException();
        }

        public bool Update(PhotoModels.Photo photo)
        {
            throw new NotImplementedException();
        }
    }
}