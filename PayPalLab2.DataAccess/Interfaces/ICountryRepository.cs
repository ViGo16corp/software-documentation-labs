using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.DataAccess.Interfaces;

public interface ICountryRepository
{
    Task<Country?> GetByCodeAsync(string code);
    Task AddAsync(Country country);
    Task SaveChangesAsync();
}
