using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class ServiceItem
    {
        public int Id { get; set; }

        // ✅ ربط الخدمة بالشركة صاحبتها
        [Required]
        public int CompanyId { get; set; }

        //  للوصول لبيانات الشركة
        [ForeignKey("CompanyId")]
        public Donor_Company? Donor_Company { get; set; }

        [Required(ErrorMessage = "اسم الخدمة مطلوب")]
        public string Name { get; set; }        // اسم الخدمة

        [Required(ErrorMessage = "القسم مطلوب")]
        public string Category { get; set; }    // القسم (خدمات صحية، أدوات مساعدة...)

        public int Usage { get; set; }          // عدد مرات الاستخدام
        public bool Available { get; set; }     // حالة التوفر (متوفر / غير متوفر)
      
      
    }
}
