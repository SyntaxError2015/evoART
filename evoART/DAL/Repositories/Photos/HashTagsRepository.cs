using System;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
using evoART.DAL.Repositories.UserAccounts;
using evoART.Models.DbModels;

namespace evoART.DAL.Repositories.Photos
{
    public class HashTagsRepository : BaseRepository<PhotoModels.HashTag>, IHashTagsRepository
    {
        public HashTagsRepository(DatabaseContext context) : base(context)
        {
        }

        public PhotoModels.HashTag[] GetPopularHashTags(int number, string name = "")
        {
            throw new NotImplementedException();
        }

        public bool Insert(string[] hashTagName, int photoId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteHashForPhoto(int photoId, int keywordId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllHashesForPhoto(int photoId)
        {
            throw new NotImplementedException();
        }
    }
}