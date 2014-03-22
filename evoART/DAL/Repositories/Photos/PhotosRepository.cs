using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class PhotosRepository : BaseRepository<PhotoModels.Photo>, IPhotosRepository
    {
        public PhotosRepository(DatabaseContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get the photo that has the entered Id
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <returns>A Photo entity</returns>
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

        /// <summary>
        /// Verify if a certain album already contains a photo with the entered name
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="albumId">The Id of the album</param>
        /// <param name="photoName">The name of the photo</param>
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

        /// <summary>
        /// Get all the photos from a certain user's album
        /// </summary>
        /// <param name="albumId">The Id of the user</param>
        /// <param name="userId">The Id of the album</param>
        /// <returns>An array of Photo entites</returns>
        public PhotoModels.Photo[] GetPhotosFromAlbum(Guid albumId, Guid userId)
        {
            try
            {
                IEnumerable<PhotoModels.Photo> photos =
                    _dbSet.Where(a => a.Album.AlbumId == albumId && a.UserAccount.UserId == userId);

                return photos.ToArray();
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Insert a photo into the database
        /// </summary>
        /// <param name="photo">A Photo entity</param>
        /// <returns>The Id of the photo that has been inserted in the database</returns>
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

        /// <summary>
        /// Insert a photo into the database
        /// </summary>
        /// <param name="photoName">The name of the photo</param>
        /// <param name="photoDescription">The description of the photo</param>
        /// <param name="albumId">The Id of the album</param>
        /// <param name="userId">The Id of the user</param>
        /// <returns>The Id of the photo that has been inserted in the database</returns>
        public Guid Insert(string photoName, string photoDescription, Guid albumId, Guid userId)
        {
            try
            {
                var photo = new PhotoModels.Photo
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

        /// <summary>
        /// Detelete a photo from the database
        /// </summary>
        /// <param name="photoId">The Id of the photo to be deleted</param>
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

        /// <summary>
        /// Update the details of a photo in the database
        /// </summary>
        /// <param name="photo">The Photo entity to update</param>
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