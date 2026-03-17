using PayPalLab2.DataAccess.Interfaces;
using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _db;

    public TransactionRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Transaction transaction)
    {
        await _db.Transactions.AddAsync(transaction);
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
