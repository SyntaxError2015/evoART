using System;
using System.Collections.Generic;
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

        public PhotoModels.HashTag[] GetPopularHashTags(int number, string name = "")
        {
            try
            {
                IEnumerable<PhotoModels.HashTag> hashTags = _dbSet.Where(h => h.HashTagName.Contains(name));
                var popularity = new int[hashTags.Count()];

                for (var i = 0; i < popularity.Length; i++)
                    popularity[i] = _dbSet.Count(h => h.HashTagId == hashTags.ElementAt(i).HashTagId);

                popularity = Sorting.SortArrayDescending(popularity, number);

                var popular = new PhotoModels.HashTag[number];

                for (var i = 0; i < number; i++)
                    popular[i] = hashTags.ElementAt(popularity[i]);

                return popular;
            }

            catch
            {
                return null;
            }
        }

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

        public bool DeleteHashTagForPhoto(Guid hashTagId, PhotoModels.Photo photo)
        {
            photo.HashTags.Remove(_dbSet.Find(hashTagId));
            var hashTag = _dbSet.Find(hashTagId);

            return DeleteHashTagForPhoto(hashTag, photo);
        }

        public bool DeleteHashTagForPhoto(Guid hashTagId, Guid photoId)
        {
            var photo = _dbContext.Photos.Find(photoId);

            return DeleteHashTagForPhoto(hashTagId, photo);
        }

        public bool DeleteAllHasheTagsForPhoto(Guid photoId)
        {
            var error = false;

            var photo = _dbContext.Photos.Find(photoId);

            while (photo.HashTags.Count > 0 && !error)
                error = DeleteHashTagForPhoto(photo.HashTags.ElementAt(0), photo);

            return !error;
        }

        public bool Insert(string[] hashTagNames, Guid photoId)
        {
            var photo = _dbContext.Photos.Find(photoId);

            try
            {
                var hashTags = new PhotoModels.HashTag[hashTagNames.Length];

                for (var i = 0; i < hashTags.Length; i++)
                {
                    hashTags[i] = new PhotoModels.HashTag
                    {
                        HashTagId = Guid.NewGuid(),
                        HashTagName = hashTagNames[i]
                    };

                    _dbSet.AddOrUpdate(hashTags[i]);
                    photo.HashTags.Add(hashTags[i]);
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

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