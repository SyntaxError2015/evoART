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
        // ReSharper disable once InconsistentNaming
        protected internal DatabaseContext _context;

        internal StandardRepository(DatabaseContext context)
        {
            this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }
    }
}