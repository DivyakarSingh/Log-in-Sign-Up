using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoLogin.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter User Name")]
        [DisplayName("User Name")]
        public string username { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public string SaltKey { get; set; }
    }
}