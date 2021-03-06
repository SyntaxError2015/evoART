﻿using System;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Social;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Social
{
    public class CommentsRepository : BaseRepository<SocialModels.Comment>, ICommentsRepository
    {
        public CommentsRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Get a certain comment
        /// </summary>
        /// <param name="commentId">The Id of the comment</param>
        /// <returns>A Comment entity</returns>
        public SocialModels.Comment GetComment(Guid commentId)
        {
            try
            {
                return _dbSet.Find(commentId);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get all the comments that a certain photo has
        /// </summary>
        /// <param name="photoId">The Id of the photo for which to fetch the comments</param>
        /// <returns>An array of Comment entities</returns>
        public SocialModels.Comment[] GetCommentsForPhoto(Guid photoId)
        {
            try
            {
                return _dbSet.Where(c => c.Photo.PhotoId == photoId)
                    .OrderBy(c => c.CommentDate)
                    .ToArray();
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Add a new comment to the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="userId">The Id of the user that made the comment</param>
        /// <param name="commentText">The text that the user has entered as the comment</param>
        public bool Insert(Guid photoId, Guid userId, string commentText)
        {
            try
            {
                var comment = new SocialModels.Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentDate = DateTime.Now,
                    CommentText = commentText,
                    Photo = _dbContext.Photos.Find(photoId),
                    UserAccount = _dbContext.UserAccounts.Find(userId)
                };

                _dbSet.Add(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new comment to the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="user">A UserAccount entity representing the user that made the comment</param>
        /// <param name="commentText">The text that the user has entered as the comment</param>
        public bool Insert(Guid photoId, AccountModels.UserAccount user, string commentText)
        {
            try
            {
                var comment = new SocialModels.Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentDate = DateTime.Now,
                    CommentText = commentText,
                    Photo = _dbContext.Photos.Find(photoId),
                    UserAccount = user
                };

                _dbSet.Add(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new comment to the database
        /// </summary>
        /// <param name="photoId">The Id of the photo</param>
        /// <param name="userName">The nickname of the user that made the comment</param>
        /// <param name="commentText">The text that the user has entered as the comment</param>
        public bool Insert(Guid photoId, string userName, string commentText)
        {
            try
            {
                var comment = new SocialModels.Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentDate = DateTime.Now,
                    CommentText = commentText,
                    Photo = _dbContext.Photos.Find(photoId),
                    UserAccount = _dbContext.UserAccounts.First(u => u.UserName == userName)
                };

                _dbSet.Add(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new comment to the database
        /// </summary>
        /// <param name="photo">A Photo entity representing the photo</param>
        /// <param name="userId">The Id of the user that made the comment</param>
        /// <param name="commentText">The text that the user has entered as the comment</param>
        public bool Insert(PhotoModels.Photo photo, Guid userId, string commentText)
        {
            try
            {
                var comment = new SocialModels.Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentDate = DateTime.Now,
                    CommentText = commentText,
                    Photo = photo,
                    UserAccount = _dbContext.UserAccounts.Find(userId)
                };

                _dbSet.Add(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new comment to the database
        /// </summary>
        /// <param name="photo">A Photo entity representing the photo</param>
        /// <param name="user">A UserAccount entity representing the user that made the comment</param>
        /// <param name="commentText">The text that the user has entered as the comment</param>
        public bool Insert(PhotoModels.Photo photo, AccountModels.UserAccount user, string commentText)
        {
            try
            {
                var comment = new SocialModels.Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentDate = DateTime.Now,
                    CommentText = commentText,
                    Photo = photo,
                    UserAccount = user
                };

                _dbSet.Add(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new comment to the database
        /// </summary>
        /// <param name="photo">A Photo entity representing the photo</param>
        /// <param name="userName">The nickname of the user that made the comment</param>
        /// <param name="commentText">The text that the user has entered as the comment</param>
        public bool Insert(PhotoModels.Photo photo, string userName, string commentText)
        {
            try
            {
                var comment = new SocialModels.Comment
                {
                    CommentId = Guid.NewGuid(),
                    CommentDate = DateTime.Now,
                    CommentText = commentText,
                    Photo = photo,
                    UserAccount = _dbContext.UserAccounts.First(u => u.UserName == userName)
                };

                _dbSet.Add(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Add a new comment to the database
        /// </summary>
        /// <param name="comment">The Commnet entity to add to the database</param>
        public bool Insert(SocialModels.Comment comment)
        {
            try                     
            {                       
                comment.CommentDate = DateTime.Now;
                comment.CommentId = Guid.NewGuid();

                _dbSet.Add(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a comment from the database
        /// </summary>
        /// <param name="commentId">The Id of the comment</param>
        public bool Delete(Guid commentId)
        {
            try
            {
                _dbSet.Remove(_dbSet.Find(commentId));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Update a comment from the database
        /// </summary>
        /// <param name="comment">The Comment entity to be updated</param>
        public bool Update(SocialModels.Comment comment)
        {
            try
            {
                _dbSet.AddOrUpdate(comment);

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}