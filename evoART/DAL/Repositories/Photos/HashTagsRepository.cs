using System;
using evoART.DAL.DbContexts;
using evoART.DAL.Interfaces.Photos;
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

        public bool Insert(string[] hashTagName, Guid photoId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteHashForPhoto(Guid photoId, Guid keywordId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAllHashesForPhoto(Guid photoId)
        {
            throw new NotImplementedException();
        }
    }
}