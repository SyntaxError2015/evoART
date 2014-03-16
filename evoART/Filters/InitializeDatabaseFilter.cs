using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using evoART.Models;

namespace evoART.Filters
{
    public class InitializeDatabaseAttribute : ActionFilterAttribute
    {
        private static DatabaseInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class DatabaseInitializer : DropCreateDatabaseAlways<DatabaseWorkUnit>
        {
            public DatabaseInitializer()
            {
                Database.SetInitializer<DatabaseWorkUnit>(null);

                try
                {
                    using (var context = new DatabaseWorkUnit())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                        
                        context.InitializeDatabaseTables();
                    }
                }

                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }
        }
    }
}