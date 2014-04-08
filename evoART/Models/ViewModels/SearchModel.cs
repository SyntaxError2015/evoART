using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace evoART.Models.ViewModels
{
    public class SearchModel
    {
        public PhotoModels.Photo[] Photos { get; set; }

        public int p { get; set; }
        public string searchText { get; set; }
    }
}