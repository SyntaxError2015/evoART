using System;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
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

        public bool VerifyExists(Guid userId, Guid albumId, string photoName)
        {
            try
            {
                return _dbSet.Count(p =>
                    p.UserAccount.UserId == userId &&
                    p.Album.AlbumId == albumId &&
                    p.PhotoName == photoName) > 0;
            }

            catch
            {
                return false;
            }
        }

        public Guid Insert(PhotoModels.Photo photo)
        {
            try
            {
                photo.PhotoId = Guid.NewGuid();

                _dbSet.Add(photo);

                return Save() ? photo.PhotoId : Guid.Empty;
            }

            catch
            {
                return Guid.Empty;
            }
        }

        public Guid Insert(string photoName, string photoDescription, Guid albumId, Guid userId)
        {
            try
            {
                var photo = new PhotoModels.Photo()
                {
                    PhotoId = Guid.NewGuid(),
                    PhotoName = photoName
                };

                _dbSet.Add(photo);

                return Save() ? photo.PhotoId : Guid.Empty;
            }

            catch
            {
                return Guid.Empty;
            }
        }

        public bool Delete(Guid photoId)
        {
            try
            {
                _dbSet.Remove(_dbSet.Find(photoId));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        public bool Update(PhotoModels.Photo photo)
        {
            try
            {
                _dbSet.AddOrUpdate(photo);

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}