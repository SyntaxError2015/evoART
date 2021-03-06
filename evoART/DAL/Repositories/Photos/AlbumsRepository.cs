﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class AlbumsRepository : BaseRepository<PhotoModels.Album>, IAlbumsRepository
    {
        public AlbumsRepository(DatabaseContext context) : base(context)
        {
        }

        /// <summary>
        /// Get a specific album
        /// </summary>
        /// <param name="albumId">The Id of the album to get</param>
        /// <returns>An Album entity</returns>
        public PhotoModels.Album GetAlbum(Guid albumId)
        {
            try
            {
                return _dbSet.Find(albumId);
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get all the albums that a user has
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="guestUserId">The Id of the user that wants to view these albums</param>
        /// <returns>An array of Album entities</returns>
        public PhotoModels.Album[] GetAlbumsForUser(Guid userId, Guid guestUserId)
        {
            try
            {
                IEnumerable<PhotoModels.Album> albums = (userId == guestUserId)
                    ? _dbSet.Where(a => a.UserAccount.UserId == userId).OrderBy(a => a.AlbumName)
                    : _dbSet.Where(a => a.UserAccount.UserId == userId && !a.IsPrivate);

                return albums.ToArray();
            }

            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Verify if an album exists or not
        /// </summary>
        /// <param name="albumId">The GUID of the album to check</param>
        /// <returns>A logical value indicating the existence of the album</returns>
        public bool VerifyExists(Guid albumId)
        {
            try
            {
                return _dbSet.Count(a => a.AlbumId == albumId) > 0;
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insert a new album in the database
        /// </summary>
        /// <param name="album">An Album entity containing all the album details</param>
        /// <param name="userId">The Id of the user</param>
        public bool Insert(PhotoModels.Album album, Guid userId)
        {
            try
            {
                album.AlbumId = Guid.NewGuid();
                album.UserAccount = _dbContext.UserAccounts.Find(userId);

                _dbSet.Add(album);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Insert a new album in the database
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="albumName">The name of the album</param>
        /// <param name="albumDescription">The description for the album</param>
        public bool Insert(Guid userId, string albumName, bool isPrivate = false, string albumDescription = "")
        {
            var album = new PhotoModels.Album
            {
                AlbumName = albumName,
                AlbumDescription = albumDescription,
                IsPrivate = isPrivate
            };

            return Insert(album, userId);
        }

        /// <summary>
        /// Detele an album from the database
        /// </summary>
        /// <param name="albumId">The Id of the album</param>
        public bool Delete(Guid albumId)
        {
            try
            {
                _dbSet.Remove(_dbSet.Find(albumId));

                return Save();
            }

            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Update a certain album from the database
        /// </summary>
        /// <param name="album">The Album entity to be updated</param>
        public bool Update(PhotoModels.Album album)
        {
            try
            {
                _dbSet.AddOrUpdate(album);

                return Save();
            }

            catch
            {
                return false;
            }
        }
    }
}