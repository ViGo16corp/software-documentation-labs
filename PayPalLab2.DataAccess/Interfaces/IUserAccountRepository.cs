using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Interfaces;

public interface IUserAccountRepository
{
    Task<UserAccount?> GetByEmailAsync(string email);
    Task AddAsync(UserAccount user);
    Task SaveChangesAsync();
}
