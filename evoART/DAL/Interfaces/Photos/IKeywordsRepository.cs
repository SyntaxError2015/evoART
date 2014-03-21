using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IKeywordsRepository
    {
        bool Insert(string keywordName);

        bool Delete(string keywordName);

        bool Update(PhotoModels.Keyword keyword);
    }
}
