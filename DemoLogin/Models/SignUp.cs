using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoLogin.Models
{
    public class SignUp
    {
        [Required (ErrorMessage="Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password Does not Match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage ="Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is Required")]

        public int PhoneNumber { get; set; }

        [Required(ErrorMessage = "Select the Gender")]
        public string Gender { get; set; }



    }
}