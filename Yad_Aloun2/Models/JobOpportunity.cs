using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class JobOpportunity // فرص العمل
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }  
        [Required]
        public string JobDescription { get; set; }


        public string JobType { get; set; }    // نوع الدوام (عن بعد، دوام كامل)
        [Required]

        public string Location { get; set; }      // المنطقة )
        [Required]

        public string Salary { get; set; }        // الراتب )
        [Required]

        public string DisabilityType { get; set; } // نوع الإعاقة (سمعية، حركية)
      

        public DateTime PostedDate { get; set; } = DateTime.Now; // تاريخ النشر

        public int? Admin_Id { get; set; }
        [ForeignKey("Admin_Id")]
        public Admin Admin { get; set; }


    }
}
