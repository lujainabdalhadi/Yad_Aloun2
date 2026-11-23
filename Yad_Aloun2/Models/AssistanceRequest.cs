using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class AssistanceRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string AttachmentUrl { get; set; } // من واجهة "المرفقات"
        [Required]
        public int UserId { get; set; } // يربط الطلب بصاحبه
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public string Status { get; set; } = "Pending";
        public string? AdminNotes { get; set; }

        public int? User_Id { get; set; }
        public int? AssignedCompanyId { get; set; }
        [ForeignKey("User_Id")]
        public User? User { get; set; }

        public int? Company_ID { get; set; }

        [ForeignKey("Company_ID")]
        public Donor_Company? DonorCompany { get; set; }
    }

}


