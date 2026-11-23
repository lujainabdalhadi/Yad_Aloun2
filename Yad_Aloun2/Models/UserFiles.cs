using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
   
        public class UserFiles
        {
            [Key]
            public int Id { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string Category { get; set; }
            [Required]
            public string DisabilityType { get; set; }
            [Required]
            public string LastSeen { get; set; }
            [Required]
            public string Status { get; set; }

            // FK
            public int User_Id { get; set; }
            [ForeignKey("User_Id")]
            public User? User { get; set; }
        public int? Admin_Id { get; set; }
        [ForeignKey("Admin_Id")]
        public Admin? Admin { get; set; }
    }
    }

