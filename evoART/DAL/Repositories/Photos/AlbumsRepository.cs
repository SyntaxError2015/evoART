using System;
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
        /// Get all the albums that a user has
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <returns></returns>
        public PhotoModels.Album[] GetAlbumsForUser(Guid userId)
        {
            try
            {
                IEnumerable<PhotoModels.Album> albums = _dbSet.Where(a => a.UserAccount.UserId == userId);

                return albums.ToArray();
            }

            catch
            {
                return null;
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
        public bool Insert(Guid userId, string albumName, string albumDescription = "")
        {
            var album = new PhotoModels.Album
            {
                AlbumName = albumName,
                AlbumDescription = albumDescription
            };

            return Insert(album, userId);
        }

        /// <summary>
        /// Detele an album from the database
        /// </summary>
        /// <param name="albumName">The name of the album</param>
        /// <param name="userId">The Id of the user that owns the album</param>
        public bool Delete(string albumName, Guid userId)
        {
            try
            {
                _dbSet.Remove(_dbSet.First(a => a.AlbumName == albumName && a.UserAccount.UserId == userId));

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