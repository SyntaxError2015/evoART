using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class CategoriesRepository : ICategoriesRepository
    {
        public bool Insert(PhotoModels.Category category)
        {
            throw new NotImplementedException();
        }

        public bool Insert(string categoryName, string categoryDescription)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string categoryName)
        {
            throw new NotImplementedException();
        }

        public bool Update(PhotoModels.Category category)
        {
            throw new NotImplementedException();
        }
    }
}