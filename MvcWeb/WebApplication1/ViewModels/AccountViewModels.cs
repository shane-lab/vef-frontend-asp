using Models;
using MvcWeb.App_Start;
using MvcWeb.Models.Promises;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcWeb.ViewModels
{
    public class LoginAccountViewModel
    {

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[^\s]+@.+\.[aA-zZ]*[^\s]$", ErrorMessage = "The entered emailaddresss does not match the pattern of a valid emailaddress")] // add email regex
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public String Password { get; set; }
    }
}