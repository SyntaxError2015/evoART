using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces;
using evoART.Models;

namespace evoART.DAL.Repositories
{
    public abstract class StandardRepository : IStandardInterface
    {
        private readonly DatabaseContext _dbContext;

        internal StandardRepository(DatabaseContext context)
        {
            this._dbContext = context;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void SaveAsync()
        {
            _dbContext.SaveChangesAsync();
        }

        #region Disposing logic

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing) return;
            
            _dbContext.Dispose();
            _disposed = true;
        }

        #endregion Disposing logic
    }
}