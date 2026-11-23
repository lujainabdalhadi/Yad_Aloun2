using System.ComponentModel.DataAnnotations;

namespace Yad_Aloun2.Models
{
    public class HomeProject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProjectName { get; set; }  
        [Required]
        public string OwnerName { get; set; }  
        [Required]
        public string Description { get; set; }  
        [Required]
        public decimal Price { get; set; }       
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string Category { get; set; }
        public string? ContactNumber { get; set; }
        //  الربط مع المستخدم
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
