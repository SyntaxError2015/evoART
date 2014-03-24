using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.DAL.UnitsOfWork;
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
        /// Get the photo from the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <returns>A Photo entity</returns>
        public PhotoModels.Photo GetPhoto(Guid photoId)
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
        /// Get the photo from the database
        /// </summary>
        /// <param name="albumId">The Id of the album in which it is found</param>
        /// <param name="photoName">The name of the photo</param>
        /// <returns>A Photo entity</returns>
        public PhotoModels.Photo GetPhoto(Guid albumId, string photoName)
        {
            try
            {
                return _dbSet.First(p => p.Album.AlbumId == albumId && p.PhotoName == photoName);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get a certain number of photos that have been added to a hashtag
        /// </summary>
        /// <param name="hashTag">The HashTag entity from where to extract the photos</param>
        /// <param name="startPosition">The 0-based index from where to select the photos</param>
        /// <param name="number">The number of photos to select</param>
        /// <returns>An array of Photo entities</returns>
        public PhotoModels.Photo[] GetPhotosForHashTag(PhotoModels.HashTag hashTag, int startPosition, int number)
        {
            try
            {
                var photos = hashTag.Photos.OrderByDescending(p => p.UploadDate);

                // If there are fewer photos than the wanted number, then return the whole collection
                if (photos.Count() < startPosition + number)
                    return photos.ToArray();

                var selection = new PhotoModels.Photo[number];

                for (var i = startPosition; i < number + startPosition; i++)
                    selection[i - startPosition] = photos.ElementAt(i);

                return selection;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get a certain number of photos that have been added to a hashtag
        /// </summary>
        /// <param name="hashTagName">The name of the hashtag from where to extract the photos</param>
        /// <param name="startPosition">The 0-based index from where to select the photos</param>
        /// <param name="number">The number of photos to select</param>
        /// <returns>An array of Photo entities</returns>
        public PhotoModels.Photo[] GetPhotosForHashTag(string hashTagName, int startPosition, int number)
        {
            try
            {
                return GetPhotosForHashTag(DatabaseWorkUnit.Instance.HashTagsRepository.GetHashTag(hashTagName),
                    startPosition, number);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Verify if a certain album already contains a photo with the entered name
        /// </summary>
        /// <param name="albumId">The Id of the album</param>
        /// <param name="photoName">The name of the photo</param>
        public bool VerifyExists(Guid albumId, string photoName)
        {
            try
            {
                return _dbSet.Count(p =>
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
        /// <returns>An array of Photo entites</returns>
        public PhotoModels.Photo[] GetPhotosFromAlbum(Guid albumId)
        {
            try
            {
                IEnumerable<PhotoModels.Photo> photos =
                    _dbSet.Where(a => a.Album.AlbumId == albumId);

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
        /// <param name="album">The Album in which to place the photo</param>
        /// <param name="contentTag">The ContentTag which classifies the photo</param>
        /// <returns>The Id of the photo that has been inserted in the database</returns>
        public Guid Insert(string photoName, string photoDescription, PhotoModels.Album album, PhotoModels.ContentTag contentTag)
        {
            try
            {
                var photo = new PhotoModels.Photo
                {
                    PhotoId = Guid.NewGuid(),
                    PhotoName = photoName,
                    PhotoDescription = photoDescription,
                    UploadDate = DateTime.Now,
                    Album = album,
                    ContentTag = contentTag
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