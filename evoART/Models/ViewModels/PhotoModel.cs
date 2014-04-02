using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;
using System.Web.UI.WebControls;
using evoART.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace evoART.Models.ViewModels
{
    public class PhotoModel
    {
        public PhotoModels.Photo Photo { get; set; }

        public SocialModels.Comment[] Comments { get; set; }

        public bool HasLiked { get; set; }

        public bool MyPhoto { get; set; }

        public Guid NextPhotoId { get; set; }
        public Guid PreviousPhotoId { get; set; }
        

        [Display(Name = "Album")]
        public string NewAlbum { get; set; }

        [Display(Name = "Content tag")]
        public string NewContentTag { get; set; }



    }
}