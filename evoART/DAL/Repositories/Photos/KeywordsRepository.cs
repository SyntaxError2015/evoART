using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.DAL.Interfaces.Photos;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class KeywordsRepository : IKeywordsRepository
    {
        public bool Insert(string keywordName)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string keywordName)
        {
            throw new NotImplementedException();
        }

        public bool Update(PhotoModels.Keyword keyword)
        {
            throw new NotImplementedException();
        }
    }
}