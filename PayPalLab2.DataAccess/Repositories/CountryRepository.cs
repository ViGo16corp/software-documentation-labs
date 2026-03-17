using Microsoft.EntityFrameworkCore;
using PayPalLab2.DataAccess.Interfaces;
using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _db;

    public CountryRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<Country?> GetByCodeAsync(string code)
    {
        return _db.Countries.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task AddAsync(Country country)
    {
        await _db.Countries.AddAsync(country);
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
