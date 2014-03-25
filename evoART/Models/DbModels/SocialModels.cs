using System;
using System.Web.DynamicData;

namespace evoART.Models.DbModels
{
    public class SocialModels
    {
        [TableName("Likes")]
        public class Like
        {
            public Guid LikeId { get; set; }

            public DateTime LikeDate { get; set; }

            public PhotoModels.Photo Photo { get; set; }

            public AccountModels.UserAccount UserAccount { get; set; }
        }

        [TableName("Comments")]
        public class Comment
        {
            public Guid CommentId { get; set; }

            public string CommentText { get; set; }

            public DateTime CommentDate { get; set; }

            public PhotoModels.Photo Photo { get; set; }

            public AccountModels.UserAccount UserAccount { get; set; }
        }
    }
}