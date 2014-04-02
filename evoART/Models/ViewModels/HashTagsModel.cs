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
    public class HashTagsModel
    {

        public PhotoModels.HashTag[] HashTags { get; set; }

        public string PhotoId { get; set; }
    }
}