using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace evoART.Models.DbModels
{
    public class PhotoModels
    {
        [TableName("Photos")]
        public class Photo
        {
            public long PhotoId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public DateTime UploadDate { get; set; }

            public AccountModels.UserAccount UserAccount { get; set; }

            public Album Album { get; set; }
        }

        [TableName("Albums")]
        public class Album
        {
            public long AlbumId { get; set; }

            public string AlbumName { get; set; }

            public string AlbumDescription { get; set; }

            public ICollection<Photo> Photos { get; set; }
        }

        [TableName("Categories")]
        public class Category
        {
            public int CategoryId { get; set; }

            public string CategoryName { get; set; }

            public string CategoryDescription { get; set; }
        }

        [TableName("Keywords")]
        public class Keyword
        {
            public long KeywordId { get; set; }

            public string KeywordName { get; set; }
        }

        [TableName("Photos_Categories")]
        public class Photo_Category
        {
            public long Photo_CategoryId { get; set; }

            public Photo Photo { get; set; }

            public Category Category { get; set; }
        }

        [TableName("Keywords_Photos")]
        public class Keyword_Photo
        {
            public long Keyword_PhotoId { get; set; }

            public Keyword Keyword { get; set; }

            public Photo Photo { get; set; }
        }
    }
}