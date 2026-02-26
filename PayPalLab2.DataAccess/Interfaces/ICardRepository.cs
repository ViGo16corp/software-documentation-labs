using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Interfaces;

public interface ICardRepository
{
    Task<Card?> GetByTokenAsync(string token);
    Task AddAsync(Card card);
    Task SaveChangesAsync();
}
