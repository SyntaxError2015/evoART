using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace evoART.DAL.DbContexts
{
    public class PhotosInitializer : DropCreateDatabaseIfModelChanges<PhotosContext>
    {
    }
}