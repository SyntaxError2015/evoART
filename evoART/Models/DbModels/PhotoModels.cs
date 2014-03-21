using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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

            public virtual AccountModels.UserAccount UserAccount { get; set; }

            public virtual Album Album { get; set; }

            //public virtual Photo_Category PhotoCategory { get; set; }
            public virtual ICollection<Category> Categories { get; set; }

            //public virtual Keyword_Photo KeywordPhoto { get; set; }
            public virtual ICollection<Keyword> Keywords { get; set; } 
        }

        [TableName("Albums")]
        public class Album
        {
            public long AlbumId { get; set; }

            public string AlbumName { get; set; }

            public string AlbumDescription { get; set; }

            public virtual ICollection<Photo> Photos { get; set; }
        }

        [TableName("Categories")]
        public class Category
        {
            public int CategoryId { get; set; }

            public string CategoryName { get; set; }

            public string CategoryDescription { get; set; }

            //public virtual Photo_Category PhotoCategory { get; set; }
            public virtual ICollection<Photo> Photos { get; set; } 
        }

        [TableName("Keywords")]
        public class Keyword
        {
            public long KeywordId { get; set; }

            public string KeywordName { get; set; }

            //public virtual Keyword_Photo KeywordPhoto { get; set; }
            public virtual ICollection<Photo> Photos { get; set; } 
        }
    }
}