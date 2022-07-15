using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DormWebApplication.Models
{
    public class User : IdentityUser
    {        
        [Display(Name = "ПІБ")]
        public string Name { get; set; }

        [Display(Name = "Рік народження")]
        public int Year { get; set; }
    }
}
