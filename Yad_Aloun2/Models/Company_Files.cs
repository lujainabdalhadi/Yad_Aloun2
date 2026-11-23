using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class Company_Files
    {
        [Key]
        public int Company_Files_Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Specialty { get; set; } // التخصص
        [Required]
        public string RegistrationNumber { get; set; } // السجل التجاري
        [Required]
        public string Status { get; set; } // معتمدة / بانتظار الاعتماد
        [Required]
        public string LogoUrl { get; set; }
        [Required]
        public string File_management { get; set; }
        public int Company_ID { get; set; }
        [ForeignKey("Company_ID")]
        public Donor_Company Donor_Company { get; set; }
        public int? Admin_Id { get; set; }
        [ForeignKey("Admin_Id")]
        public Admin Admin { get; set; }
    }
}
