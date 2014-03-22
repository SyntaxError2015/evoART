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

        public bool Insert(PhotoModels.Album album)
        {
            try
            {
                album.AlbumId = Guid.NewGuid();

                _dbSet.Add(album);

                return Save();
            }

            catch
            {
                return false;
            }
        }

        public bool Insert(string albumName, string albumDescription = "")
        {
            var album = new PhotoModels.Album()
            {
                AlbumName = albumName,
                AlbumDescription = albumDescription
            };

            return Insert(album);
        }

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