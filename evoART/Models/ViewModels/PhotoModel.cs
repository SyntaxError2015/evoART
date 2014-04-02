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


        public SocialModels.Like MyLike { get; set; }
        public bool HasLiked { get; set; }

        public bool MyPhoto { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Photo name")]
        public string NewPhotoName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Photo description")]
        public string NewPhotoDescription { get; set; }


        [Display(Name = "Select an album")]
        public string Album { get; set; }



    }
}