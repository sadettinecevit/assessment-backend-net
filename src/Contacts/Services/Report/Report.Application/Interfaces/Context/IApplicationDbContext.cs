using Microsoft.EntityFrameworkCore;
using Report.Domain.Entities;

namespace Report.Application.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        DbSet<ReportLog> ReportLogs { get; set; }
        DbSet<ReportLogStatus> ReportLogStatuses { get; set; }
    }
}
