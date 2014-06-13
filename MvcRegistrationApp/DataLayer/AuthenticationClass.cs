using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public class AuthenticationClass
    {
       
        public int UserId { get; set; }
       [Required(ErrorMessage = "*")]
        public string UserName { get; set; }
       [Required(ErrorMessage = "*")]
        public string Password { get; set; }
        public string CreatedDate { get; set; }
    }
}
