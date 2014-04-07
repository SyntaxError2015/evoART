using System.ComponentModel.DataAnnotations;
using evoART.Models.DbModels;

namespace evoART.Models.ViewModels
{
    public class IndexModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public PhotoModels.Photo[] TopPhotos { get; set; }

    }
}