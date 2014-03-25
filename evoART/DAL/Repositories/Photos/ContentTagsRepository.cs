using System;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class ContentTagsRepository : BaseRepository<PhotoModels.ContentTag>, IContentTagsRepository
    {
        public ContentTagsRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Get a certain content tag
        /// </summary>
        /// <param name="contentTagId">The Id of the content tag to get</param>
        /// <returns>A ContentTag entity</returns>
        public PhotoModels.ContentTag GetContentTag(Guid contentTagId)
        {
            try
            {
                return _dbSet.Find(contentTagId);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get a certain content tag
        /// </summary>
        /// <param name="contentTagName">The name of the content tag to get</param>
        /// <returns>A ContentTag entity</returns>
        public PhotoModels.ContentTag GetContentTag(string contentTagName)
        {
            try
            {
                return _dbSet.First(t => t.ContentTagName == contentTagName);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get all the content tags in the database
        /// </summary>
        /// <returns>An array of ContentTag entities</returns>
        public PhotoModels.ContentTag[] GetAllContentTags()
        {
            try
            {
                return _dbSet.ToArray();
            }

            catch
            {
                return null;
            }
        }
    }
}