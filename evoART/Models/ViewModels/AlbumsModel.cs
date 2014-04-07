using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace evoART.Models.ViewModels
{
    public class AlbumsModel
    {
        public PhotoModels.Album[] Albums { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Album name")]
        public string NewAlbumName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album description")]
        public string NewAlbumDescription { get; set; }

        public string AlbumsUser { get; set; }
        
        [Display(Name = "Private album")]
        public bool Private { get; set; }

    }
}