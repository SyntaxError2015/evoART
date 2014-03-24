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
            public Guid PhotoId { get; set; }

            public string PhotoName { get; set; }

            public string PhotoDescription { get; set; }

            public DateTime UploadDate { get; set; }

            public virtual Album Album { get; set; }

            public virtual ContentTag ContentTag { get; set; }

            public virtual ICollection<HashTag> HashTags { get; set; }

            public virtual ICollection<SocialModels.Like> Likes { get; set; }

            public virtual ICollection<SocialModels.Comment> Comments { get; set; } 
        }

        [TableName("Albums")]
        public class Album
        {
            public Guid AlbumId { get; set; }

            public string AlbumName { get; set; }

            public string AlbumDescription { get; set; }

            public virtual ICollection<Photo> Photos { get; set; }

            public virtual AccountModels.UserAccount UserAccount { get; set; }
        }

        [TableName("HashTags")]
        public class HashTag
        {
            public Guid HashTagId { get; set; }

            public string HashTagName { get; set; }

            public virtual ICollection<Photo> Photos { get; set; } 
        }

        [TableName("ContentTags")]
        public class ContentTag
        {
            public Guid ContentTagId { get; set; }

            public string ContentTagName { get; set; }

            public string ContentTagDescription { get; set; }

            public ICollection<Photo> Photos { get; set; } 
        }
    }
}