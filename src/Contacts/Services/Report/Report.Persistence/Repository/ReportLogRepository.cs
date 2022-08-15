using Report.Application.Interfaces.Repositories;
using Report.Domain.Entities;
using Report.Persistence.Context;

namespace Report.Persistence.Repository
{
    public class ReportLogRepository : Repository<ReportLog>, IReportLogRepository
    {
        public ReportLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
