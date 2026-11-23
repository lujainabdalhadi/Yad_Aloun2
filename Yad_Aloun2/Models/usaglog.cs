using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class usaglog
    {
        [Key]
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Donor_Company? Donor_Company { get; set; }
        public string ServiceName { get; set; }
        public string BeneficiaryName { get; set; }
        public DateTime UsageDate { get; set; }
        public int Rating { get; set; }
    }
}
