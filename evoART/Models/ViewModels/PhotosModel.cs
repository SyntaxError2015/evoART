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
    public class PhotosModel
    {
        public PhotoModels.Album[] Albums { get; set; }
        public PhotoModels.ContentTag[] ContentTags { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Photo name")]
        public string NewPhotoName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Photo description")]
        public string NewPhotoDescription { get; set; }

        public string PhotoId { get; set; }

        [Display(Name = "Select an album")]
        public string Album { get; set; }
    }
}