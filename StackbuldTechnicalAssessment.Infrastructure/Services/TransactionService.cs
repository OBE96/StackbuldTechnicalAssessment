using Microsoft.EntityFrameworkCore.Storage;
using StackbuldTechnicalAssessment.Infrastructure.Context;
using StackbuldTechnicalAssessment.Infrastructure.Services.Interfaces;


namespace StackbuldTechnicalAssessment.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction _currentTransaction;

        public TransactionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
                return _currentTransaction;

            _currentTransaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _dbContext.SaveChangesAsync(cancellationToken);
                    await _currentTransaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.RollbackAsync(cancellationToken);
                }
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    await _currentTransaction.DisposeAsync();
                    _currentTransaction = null;
                }
            }
        }
    }
}
