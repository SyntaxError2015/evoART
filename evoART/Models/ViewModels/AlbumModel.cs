using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace evoART.Models.ViewModels
{
    public class AlbumModel
    {
        public PhotoModels.Photo[] Photos { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Album name")]
        public string AlbumName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Album description")]
        public string AlbumDescription { get; set; }
        public string AlbumId { get; set; }
        public string UserName { get; set; }
        public int MyAlbum { get; set; }

        [Display(Name = "Private album")]
        public bool Private { get; set; }

    }
}