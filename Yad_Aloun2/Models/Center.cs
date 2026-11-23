using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yad_Aloun2.Models
{
    public class Center
    {
        [Key]
        public int Center_Id { get; set; }
        [Required]
        public string CenterName { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Status { get; set; }
       
        public string? Address { get; set; } // العنوان التفصيلي
  
        public string? PhoneNumber { get; set; } // لزر "اتصال" الموجود في الصورة

        // إحداثيات الخريطة
        public double? Latitude { get; set; }//خط العرض 
        public double? Longitude { get; set; }//خط الطول)

        // نوع الخدمة ( الكل، مراكز تأهيل، رعاية صحية)
        [Required]
        public string Category { get; set; }
        [ValidateNever]
        public int ?Admin_Id { get; set; }
        [ForeignKey("Admin_Id")]
        public Admin ?Admin { get; set; }
        public int? CompanyId { get; set; } // وضعنا علامة ؟ لأن بعض المراكز قد تتبع للأدمن مباشرة وليس لشركة
                                            //يا فيجوال ستوديو، اسمح لهذا الحقل إنه يكون فارغ إذا ما في شركة للمركز". لو ما حطينا ? البرنامج رح يرفض يحفظ أي مركز إلا إذا ربطته بشركة
        [ValidateNever]
        public  Donor_Company Company { get; set; }



    }
}
