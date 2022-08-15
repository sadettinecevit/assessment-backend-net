using Microsoft.EntityFrameworkCore.Storage;
using Report.Application.Interfaces.Repositories;

namespace Report.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkService : IAsyncDisposable
    {
        Task<IDbContextTransaction> BeginTansactionAsync();
        public IReportLogRepository ReportLogRepository { get; }
        public IReportLogStatusRepository ReportLogStatusRepository { get; }
    }
}
