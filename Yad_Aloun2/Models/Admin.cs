using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class Admin
    {
        [Key]
        public int Admin_Id { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string AdminName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please Enter Your Age")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email")]
        [Phone]
        public string PhoneNumber { get; set; }
        public string? Certificates_Path { get; set; } = null;
     
        public string Location { get; set; }
        [Required(ErrorMessage = "Please Enter Your Admin Password")]
        public string AdminPassword { get; set; }
        [NotMapped]
        [Compare("AdminPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public bool IsLoggedIn { get; set; } = false;
        public string? CV_Path { get; set; }

        public string? ProfileImagePath { get; set; }
        [ValidateNever]
        public string Role { get; set; } = "Admin";

        [ValidateNever]

        public ICollection<Inquiry> Inquiries { get; set; }
        [ValidateNever]

        public ICollection<Center> Center { get; set; }


    }




}

