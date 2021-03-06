﻿using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;
using evoART.Special;

namespace evoART.DAL.Repositories.Photos
{
    public class HashTagsRepository : BaseRepository<PhotoModels.HashTag>, IHashTagsRepository
    {
        public HashTagsRepository(DatabaseContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Get a certain hashtag
        /// </summary>
        /// <param name="hashTagName">The name of the hashtag</param>
        /// <returns>A HashTag entity</returns>
        public PhotoModels.HashTag GetHashTag(string hashTagName)
        {
            try
            {
                return _dbSet.First(h => h.HashTagName == hashTagName);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get a the list with the most popular tags on the website. The user can specify what the hashtags name should contain.
        /// </summary>
        /// <param name="number">The number of tags to return</param>
        /// <param name="name">(Optional) The substring that all the returned hashtag names should contain</param>
        /// <returns>A HashTag entity array</returns>
        public PhotoModels.HashTag[] GetPopularHashTags(int number, string name = "")
        {
            try
            {
                PhotoModels.HashTag[] hashTags = _dbSet.Where(h => h.HashTagName.Contains(name)).ToArray();
                var popularity = new int[hashTags.Count()];

                if (hashTags.Length < number)
                    number = hashTags.Length;

                var k = 0;
                //for (var i = 0; i < popularity.Length; i++)
                //    popularity[i] = _dbSet.Count(h => h.HashTagId == hashTags[i].HashTagId);

                foreach (var ht in hashTags)
                {
                    popularity[k] = _dbSet.Count(h => h.HashTagId == ht.HashTagId) - 1;
                    k++;
                }

                var sorted = Sorting.SortArrayDescending(popularity, number);

                var popular = new PhotoModels.HashTag[number];

                for (var i = 0; i < number; i++)
                    popular[i] = hashTags[sorted[i]];

                return popular;
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Delete a an association between a hashtag and a photo
        /// </summary>
        /// <param name="hashTag">The HashTag entity for which to beak the association</param>
        /// <param name="photo">The Photo entity for which to beak the association</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool DeleteHashTagForPhoto(PhotoModels.HashTag hashTag, PhotoModels.Photo photo)
        {
            try
            {
                photo.HashTags.Remove(hashTag);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a an association between a hashtag and a photo
        /// </summary>
        /// <param name="hashTagId">The Id of the hashtag</param>
        /// <param name="photo">The Photo entity for which to beak the association</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool DeleteHashTagForPhoto(Guid hashTagId, PhotoModels.Photo photo)
        {
            var tags = _dbSet.Where(h => h.HashTagId == hashTagId).ToArray();

            return tags.All(tag => DeleteHashTagForPhoto(tag, photo));
        }

        /// <summary>
        /// Delete a an association between a hashtag and a photo
        /// </summary>
        /// <param name="hashTagId">The Id of the hashtag</param>
        /// <param name="photoId">The Id of the photo</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool DeleteHashTagForPhoto(Guid hashTagId, Guid photoId)
        {
            var photo = _dbContext.Photos.Find(photoId);

            return DeleteHashTagForPhoto(hashTagId, photo);
        }

        /// <summary>
        /// Delete a an association between a hashtag and a photo
        /// </summary>
        /// <param name="hashTagName">The name of the hashtag to delete</param>
        /// <param name="photo">The Photo entity for which to delete the hashtag</param>
        /// <returns></returns>
        public bool DeleteHashTagForPhoto(string hashTagName, PhotoModels.Photo photo)
        {
            try
            {
                var hashtagId = _dbSet.First(h => h.HashTagName == hashTagName).HashTagId;

                return DeleteHashTagForPhoto(hashtagId, photo.PhotoId);
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete all the association that a photo has with all its hashtags
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <returns>A bool value indicating the success of the action</returns>
        public bool DeleteAllHasheTagsForPhoto(Guid photoId)
        {
            var error = false;

            var photo = _dbContext.Photos.Find(photoId);

            while (photo.HashTags.Count > 0 && !error)
                error = DeleteHashTagForPhoto(photo.HashTags.ElementAt(0), photo);

            return !error;
        }

        /// <summary>
        /// Insert a new hashtag into the database
        /// </summary>
        /// <param name="hashTagName">An array of strings representing the hashtag names</param>
        /// <param name="photo">The Photo entity for which to add the hashtag</param>
        public bool Insert(string hashTagName, PhotoModels.Photo photo)
        {
            try
            {
                var tag = _dbSet.FirstOrDefault(t => t.HashTagName == hashTagName);

                if (tag != null)
                {
                    tag.Photos.Add(photo);

                    _dbSet.AddOrUpdate(tag);
                }

                else
                {
                    var newTag = new PhotoModels.HashTag
                    {
                        HashTagId = Guid.NewGuid(),
                        HashTagName = hashTagName,
                        Photos = new Collection<PhotoModels.Photo> {photo}
                    };

                    _dbSet.Add(newTag);

                }

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insert new hashtags into the database
        /// </summary>
        /// <param name="hashTagNames">An array of strings representing the hashtag names</param>
        /// <param name="photoId">The Id of the photo for which to add these hashtags</param>
        public bool Insert(string[] hashTagNames, Guid photoId)
        {
            var photo = _dbContext.Photos.Find(photoId);

            try
            {
                var hashTags = new PhotoModels.HashTag[hashTagNames.Length];

                for (var i = 0; i < hashTags.Length; i++)
                {
                    if(!Insert(hashTagNames[i], photo))
                        throw new Exception();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a certain hashtag from the server
        /// </summary>
        /// <param name="hashTagId">The Id of the hashtag to delete</param>
        public bool DeleteHashTag(Guid hashTagId)
        {
            try
            {
                _dbSet.Remove(_dbSet.Find(hashTagId));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a certain hashtag from the server
        /// </summary>
        /// <param name="hashTagName">The name of the hashtag to delete</param>
        public bool DeleteHashTag(string hashTagName)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(h => h.HashTagName == hashTagName));

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}