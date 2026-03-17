using PayPalLab2.Business.Interfaces;
using PayPalLab2.DataAccess.Csv;
using PayPalLab2.DataAccess.Interfaces;
using PayPalLab2.DataAccess.Models;

namespace PayPalLab2.Business.Services;

public class ImportService : IImportService
{
    private readonly ICsvReader<TopUpCsvRow> _csvReader;
    private readonly IUserAccountRepository _users;
    private readonly ICountryRepository _countries;
    private readonly ICardRepository _cards;
    private readonly ITransactionRepository _tx;

    public ImportService(
        ICsvReader<TopUpCsvRow> csvReader,
        IUserAccountRepository users,
        ICountryRepository countries,
        ICardRepository cards,
        ITransactionRepository tx)
    {
        _csvReader = csvReader;
        _users = users;
        _countries = countries;
        _cards = cards;
        _tx = tx;
    }

    public async Task ImportTopUpsAsync(string csvPath)
    {
        var rows = _csvReader.Read(csvPath);

        foreach (var row in rows)
        {
            // 1) Country (upsert by Code)
            var country = await _countries.GetByCodeAsync(row.IssuerCountryCode);
            if (country is null)
            {
                country = new Country
                {
                    Code = row.IssuerCountryCode,
                    Name = row.IssuerCountryName
                };
                await _countries.AddAsync(country);
                await _countries.SaveChangesAsync();
            }

            // 2) User (upsert by Email)
            var user = await _users.GetByEmailAsync(row.Email);
            if (user is null)
            {
                user = new UserAccount
                {
                    Email = row.Email,
                    PasswordHash = "imported_hash",
                    Status = AccountStatus.Active,
                    Profile = new Profile
                    {
                        FullName = row.FullName,
                        Phone = row.Phone
                    }
                };

                await _users.AddAsync(user);
                await _users.SaveChangesAsync();
            }
            else
            {
                // optional: update profile if changed
                if (user.Profile is null)
                {
                    user.Profile = new Profile { FullName = row.FullName, Phone = row.Phone };
                    await _users.SaveChangesAsync();
                }
            }

            // 3) Card (upsert by Token)
            var card = await _cards.GetByTokenAsync(row.CardToken);
            if (card is null)
            {
                card = new Card
                {
                    Token = row.CardToken,
                    MaskedPan = row.MaskedPan,
                    Expiry = row.Expiry,
                    Brand = row.Brand,
                    IsDefault = false,
                    UserAccountId = user.Id,
                    CountryId = country.Id
                };

                await _cards.AddAsync(card);
                await _cards.SaveChangesAsync();
            }

            // 4) Transaction (always insert)
            var tx = new Transaction
            {
                Amount = row.Amount,
                CreatedAt = row.CreatedAt,
                Status = TxStatus.Success,
                UserAccountId = user.Id,
                PaymentMethodId = card.Id
            };

            await _tx.AddAsync(tx);
            await _tx.SaveChangesAsync();
        }
    }
}
