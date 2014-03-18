using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.DbContexts;
using evoART.Models;

namespace evoART.DAL.Repositories
{
    public abstract class StandardRepository
    {
        private DatabaseContext _context;

        protected internal StandardRepository(DatabaseContext context)
        {
            this._context = context;
        }
    }
}