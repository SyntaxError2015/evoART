﻿using System.Data.Entity;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;

namespace evoART.DAL.Repositories
{
    public abstract class BaseRepository<T> : IBaseInterface where T : class
    {
        internal readonly DatabaseContext _dbContext;
        internal readonly DbSet<T> _dbSet;

        internal BaseRepository(DatabaseContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        #region Persistence logic

        public bool Save()
        {
            try
            {
                _dbContext.SaveChanges();

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool SaveAsync()
        {
            try
            {
                _dbContext.SaveChangesAsync();

                return true;
            }

            catch
            {
                return false;
            }
        }

        #endregion
    }
}