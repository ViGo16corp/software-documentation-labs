using Microsoft.EntityFrameworkCore;
using PayPalLab2.DataAccess.Interfaces;
using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Repositories;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly AppDbContext _db;

    public UserAccountRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<UserAccount?> GetByEmailAsync(string email)
    {
        return _db.UserAccounts
            .Include(u => u.Profile)
            .Include(u => u.PaymentMethods)
            .Include(u => u.Transactions)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(UserAccount user)
    {
        await _db.UserAccounts.AddAsync(user);
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
