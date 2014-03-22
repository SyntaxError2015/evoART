using System;
using System.Collections.Generic;
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

            public virtual ICollection<HashTag> HashTags { get; set; } 
        }

        [TableName("Albums")]
        public class Album
        {
            public long AlbumId { get; set; }

            public string AlbumName { get; set; }

            public string AlbumDescription { get; set; }

            public virtual ICollection<Photo> Photos { get; set; }
        }

        [TableName("HashTags")]
        public class HashTag
        {
            public long HashTagId { get; set; }

            public string HashTagName { get; set; }

            public virtual ICollection<Photo> Photos { get; set; } 
        }
    }
}