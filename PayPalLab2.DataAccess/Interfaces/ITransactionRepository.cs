using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task SaveChangesAsync();
}
