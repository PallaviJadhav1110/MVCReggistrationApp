using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcRegistrationApp.Models
{
    public class loginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string password { get; set; }

    }
}