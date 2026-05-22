using Microsoft.EntityFrameworkCore;
using PayPalLab2.DataAccess.Interfaces;
using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _db;

    public CardRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<Card?> GetByTokenAsync(string token)
    {
        return _db.Cards
            .Include(c => c.Country)
            .FirstOrDefaultAsync(c => c.Token == token);
    }

    public async Task AddAsync(Card card)
    {
        await _db.Cards.AddAsync(card);
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
