using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using evoART.Models.DbModels;
using System.ComponentModel.DataAnnotations;

namespace evoART.Models.ViewModels
{
    public class ProfileModel
    {
        public AccountModels.UserAccount UserDetails { get; set; }

        public bool MyProfile { get; set; }

        public bool FacebookLinked { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordRepeat { get; set; }
    }
}