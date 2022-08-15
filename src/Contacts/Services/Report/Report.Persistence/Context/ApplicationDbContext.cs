using Microsoft.EntityFrameworkCore;
using Report.Application.Interfaces.Context;
using Report.Domain.Entities;

namespace Report.Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ReportLog> ReportLogs { get; set; }
        public DbSet<ReportLogStatus> ReportLogStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReportLogStatus>().HasData(new ReportLogStatus[]
            {
                new ReportLogStatus() {UUID = 1, Status = "Talep alındı."},
                new ReportLogStatus() {UUID = 2, Status = "Hazırlanıyor."},
                new ReportLogStatus() {UUID = 3, Status = "Hazırlandı."}
            });

        }
    }
}
