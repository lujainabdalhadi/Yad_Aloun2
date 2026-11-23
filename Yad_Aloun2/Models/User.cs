using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Yad_Aloun2.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage ="Please Enter Your Name")]
        public string FullName { get; set; }
        public int Age { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        [Required]
        public string Phone { get; set; }
       
        [Required]
        public string location { get; set; }
    
        [Required]
        public string FamilyPhoneNumber { get; set; }
        [Required]
        public string DisabilityType { get; set; }

        [ValidateNever]
        public string Role { get; set; } = "User";
        [ValidateNever]
        public ICollection<HomeProject> HomeProject { get; set; }
        [ValidateNever]
        public ICollection<AssistanceRequest> assistanceRequests { get; set; }

    }
}
