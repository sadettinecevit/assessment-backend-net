using Report.Application.Interfaces.Repositories;
using Report.Domain.Entities;
using Report.Persistence.Context;

namespace Report.Persistence.Repository
{
    public class ReportLogStatusRepository : Repository<ReportLogStatus>, IReportLogStatusRepository
    {
        public ReportLogStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
