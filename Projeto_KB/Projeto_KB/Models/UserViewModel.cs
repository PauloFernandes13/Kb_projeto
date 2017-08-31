using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto_KB.Models
{
    public class UserViewModel
    {

        public UserViewModel() { }

        public UserViewModel(ApplicationUser user) 
        {
            Id = user.Id;
            Email = user.Email;
            UserName = user.UserName;
            PhoneNumber = user.PhoneNumber;
            AccountName = user.AccountName;
            ContactName = user.ContactName;
            Country = user.Country;
            



        }


        public string Id { get; set; }
        public string Email { get; set; }
        [Display(Name = "User")]
        public string UserName { get; set; }
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }
        [Display (Name = "Account" )]
        public string AccountName { get; set; }
        [Display(Name = "Contact")]
        public string ContactName { get; set; }
        public string Country { get; set; }
    }
}

