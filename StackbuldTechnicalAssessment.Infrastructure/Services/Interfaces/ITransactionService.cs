using Microsoft.EntityFrameworkCore.Storage;


namespace StackbuldTechnicalAssessment.Infrastructure.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
