using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface ICategoriesRepository
    {
        bool Insert(PhotoModels.Category category);

        bool Insert(string categoryName, string categoryDescription);

        bool Delete(string categoryName);

        bool Update(PhotoModels.Category category);
    }
}
