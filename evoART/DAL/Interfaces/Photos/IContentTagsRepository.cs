using System;
using evoART.Models.DbModels;

namespace evoART.DAL.Interfaces.Photos
{
    interface IContentTagsRepository
    {
        PhotoModels.ContentTag GetContentTag(Guid contentTagId);

        PhotoModels.ContentTag GetContentTag(string contentTagName);

        PhotoModels.ContentTag[] GetAllContentTags();
    }
}
