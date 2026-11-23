using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class Inquiry //الرد على استفسارات
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string SenderName { get; set; }
        [Required]
        public string MessageContent { get; set; }
        [Required]
        public string? AdminReply { get; set; }
        [Required]
        public string SenderEmail { get; set; }
       
        public int? Admin_Id { get; set; }
        [ForeignKey("Admin_Id")]
        public Admin Admin { get; set; }
        [ValidateNever]
        public int? User_Id { get; set; }

        [ForeignKey("User_Id")]
        public User? User { get; set; }
    }
}
