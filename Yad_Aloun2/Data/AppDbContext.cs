using Microsoft.EntityFrameworkCore;
using Yad_Aloun2.Models;

namespace Yad_Aloun2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<AssistanceRequest> AssistanceRequest { get; set; }
         public DbSet<Center> Center { get; set; }
         public DbSet<Company_Files> Company_Files { get; set; }
        public DbSet<Donor_Company> Donor_Company { get; set; }
        public DbSet<HomeProject> HomeProject { get; set; }
         public DbSet<JobOpportunity> JobOpportunity { get; set; }
         public DbSet<SupportActivity> SupportActivity { get; set; }
        public DbSet<Inquiry> Inquiry { get; set; }
        public DbSet<UserFiles> UserFile { get; set; }
        public DbSet<usaglog> usaglog { get; set; }

        public DbSet<ServiceItem> ServiceItems { get; set; }
        
    }

}