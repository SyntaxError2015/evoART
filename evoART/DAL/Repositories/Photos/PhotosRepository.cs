﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Services.Description;
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
        /// Get a portion of the most popular photos on the website
        /// </summary>
        /// <param name="startPosition">The position from where to start returning photos</param>
        /// <param name="number">The number of photos to return</param>
        /// <returns>An array of Photo entities</returns>
        public PhotoModels.Photo[] GetPopularPhotos(int startPosition, int number)
        {
            try
            {
                IEnumerable<PhotoModels.Photo> photos;
                var numberOfDays = 5;
                var existent = _dbSet.Count();

                do
                {

                    var limitDate = DateTime.Now.Subtract(new TimeSpan(numberOfDays, 0, 0, 0));

                    photos = _dbSet.OrderByDescending(p => p.Likes.Count*5 + p.Comments.Count*5 + p.Views)
                        .Where(p => p.UploadDate > limitDate);
       
                    numberOfDays++;

                } while (photos.Count() < number && existent >= number);

                return SelectPhotosByPositionAndNumber(photos, startPosition, number);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get a portion of the most popular photos that a user has
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="startPosition">The position from where to start returning photos</param>
        /// <param name="number">The number of photos to return</param>
        /// <returns>An array of Photo entities</returns>
        public PhotoModels.Photo[] GetPopularPhotosForUser(Guid userId, int startPosition, int number)
        {
            try
            {
                var photos =
                    _dbSet.Where(u => u.Album.UserAccount.UserId == userId)
                        .OrderByDescending(p => p.Likes.Count * 5 + p.Comments.Count * 5 + p.Views);

                return SelectPhotosByPositionAndNumber(photos, startPosition, number);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get a portion of the most popular photos that a user has
        /// </summary>
        /// <param name="userName">The nickname of the user</param>
        /// <param name="startPosition">The position from where to start returning photos</param>
        /// <param name="number">The number of photos to return</param>
        /// <returns>An array of Photo entities</returns>
        public PhotoModels.Photo[] GetPopularPhotosForUser(string userName, int startPosition, int number)
        {
            try
            {
                var photos =
                    _dbSet.Where(u => u.Album.UserAccount.UserName == userName)
                        .OrderByDescending(p => p.Likes.Count * 5 + p.Comments.Count * 5 + p.Views);

                return SelectPhotosByPositionAndNumber(photos, startPosition, number);
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
                var tags = DatabaseWorkUnit.Instance.HashTagsRepository.GetPopularHashTags(10, hashTag.HashTagName);

                IEnumerable<PhotoModels.Photo> photos = new Collection<PhotoModels.Photo>();// = hashTag.Photos.OrderByDescending(p => p.UploadDate);

                photos = tags.Aggregate(photos, (current, tag) => current.Union(tag.Photos.OrderByDescending(p => p.UploadDate)));

                return SelectPhotosByPositionAndNumber(photos, startPosition, number);
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
        /// Increment the number of views that a photo has
        /// </summary>
        /// <param name="photoId"></param>
        public void IncrementViews(Guid photoId)
        {
            try
            {
                var photo = _dbSet.Find(photoId);

                photo.Views++;

                _dbSet.AddOrUpdate(photo);

                Save();
            }

// ReSharper disable once EmptyGeneralCatchClause
            catch
            {
                //nothing to do here
            }
        }

        /// <summary>
        /// Verify if a photo exists in the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        public bool VerifyExists(Guid photoId)
        {
            try
            {
                return _dbSet.Count(p => p.PhotoId == photoId) > 0;
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
                    _dbSet.Where(a => a.Album.AlbumId == albumId).OrderBy(d => d.UploadDate);

                return photos.ToArray();
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get the previous photo from a sequence that belongs to a certain album
        /// </summary>
        /// <param name="currentPhoto">The Photo entity to use as a reference point</param>
        /// <returns>A Photo entity</returns>
        public Guid GetPreviousPhoto(PhotoModels.Photo currentPhoto)
        {
            try
            {
                var photo = _dbSet.Where(p => p.Album.AlbumId == currentPhoto.Album.AlbumId)
                    .OrderByDescending(d => d.UploadDate)
                    .FirstOrDefault(d => d.UploadDate < currentPhoto.UploadDate);

                return photo.PhotoId != currentPhoto.PhotoId ? photo.PhotoId : Guid.Empty;
            }

            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Get the next photo from a sequence that belongs to a certain album
        /// </summary>
        /// <param name="currentPhoto">The Photo entity to use as a reference point</param>
        /// <returns>A Photo entity</returns>
        public Guid GetNextPhoto(PhotoModels.Photo currentPhoto)
        {
            try
            {
                var photo = _dbSet.Where(p => p.Album.AlbumId == currentPhoto.Album.AlbumId)
                    .OrderBy(d => d.UploadDate)
                    .FirstOrDefault(d => d.UploadDate > currentPhoto.UploadDate);

                return photo.PhotoId != currentPhoto.PhotoId ? photo.PhotoId : Guid.Empty;
            }

            catch
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Insert a photo into the database
        /// </summary>
        /// <param name="photo">A Photo entity that needs to have set the following things:
        /// * Name (optional),
        /// * Description (optional),
        /// * Album,
        /// * ContentTag.
        /// The rest of the fields are initialized with default values</param>
        /// <returns>The Id of the photo that has been inserted in the database</returns>
        public Guid Insert(PhotoModels.Photo photo)
        {
            try
            {
                photo.PhotoId = Guid.NewGuid();
                photo.Views = 0;
                photo.UploadDate = DateTime.Now;

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
                    Views = 0,
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
                var photo = _dbSet.Find(photoId);

                _dbSet.Remove(photo);

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

        #region Private auxiliary functions

        private static PhotoModels.Photo[] SelectPhotosByPositionAndNumber(IEnumerable<PhotoModels.Photo> photos, int startPosition, int number)
        {
            PhotoModels.Photo[] selection;

            var enumerable = photos as PhotoModels.Photo[] ?? photos.ToArray();
            var count = enumerable.Count();

            // If there are fewer photos than the wanted number, then return only what is possible
            if (count < startPosition + number)
            {
                if (startPosition >= count)
                    return null;

                selection = new PhotoModels.Photo[count - startPosition];

                for (var i = startPosition; i < count; i++)
                    selection[i - startPosition] = enumerable.ElementAt(i);
            }

            else
            {
                selection = new PhotoModels.Photo[number];

                for (var i = startPosition; i < number + startPosition; i++)
                    selection[i - startPosition] = enumerable.ElementAt(i);
            }

            return selection;
        }

        #endregion
    }
}