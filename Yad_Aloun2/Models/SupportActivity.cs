using System.ComponentModel.DataAnnotations;

namespace Yad_Aloun2.Models
{
    public class SupportActivity
    {
        [Key]
        
        public int Id { get; set; }
        [Required]
        public string Activity_Title { get; set; }   
        [Required]
        public string Description { get; set; } 
        [Required]
        public string IconPath { get; set; }    
        [Required]

        // هل تتبع لمركز معين؟
        public int? CenterId { get; set; }
        public virtual Center Center { get; set; }
    }
}
