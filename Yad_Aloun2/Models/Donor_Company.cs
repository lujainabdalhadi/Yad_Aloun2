using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Yad_Aloun2.Models
{
    public class Donor_Company
    {
        [Key]
        public int Company_ID { get; set; }
        [Required(ErrorMessage ="Please Enter your Company Name")]
        public string CompanyName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
            [Phone]
        public string Phone { get; set; }
      
        [Required]
        public string Company_Address { get; set; }
      
        [Required]
        public List<string?> SelectedServices { get; set; }// لتمثيل الخدمات التي تم اختيارها من قبل الشركة
          [ValidateNever]  // الشركة الواحدة يمكن أن يكون لها مجموعة من المراكز أو الخدمات
        public string Role { get; set; } = "Supporter";
        [ValidateNever]
        public ICollection<ServiceItem> ServiceItems { get; set; } = new List<ServiceItem>();
        [ValidateNever]
        public ICollection<AssistanceRequest?> RequestsAssistance { get; set; }
    }
}
