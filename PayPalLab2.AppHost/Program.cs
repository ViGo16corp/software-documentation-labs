using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayPalLab2.DataAccess;
using PayPalLab2.DataAccess.Interfaces;
using PayPalLab2.DataAccess.Repositories;
using PayPalLab2.DataAccess.Csv;
using PayPalLab2.Business.Interfaces;
using PayPalLab2.Business.Services;

// ================= Paths (diagnostics) =================
var dbPath = Path.GetFullPath("paypal.db");
var csvPath = Path.GetFullPath("topups.csv");

Console.WriteLine($"DB path : {dbPath}");
Console.WriteLine($"CSV path: {csvPath}");
Console.WriteLine($"CSV exists: {File.Exists(csvPath)}");

if (File.Exists(csvPath))
{
    var lines = File.ReadLines(csvPath).Count(); // includes header
    Console.WriteLine($"CSV lines (incl header): {lines}");
}
else
{
    Console.WriteLine("CSV file not found. Put topups.csv in the solution root or pass absolute path.");
}

// ================= DI =================
var services = new ServiceCollection();

// DB
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// DAL
services.AddScoped<IUserAccountRepository, UserAccountRepository>();
services.AddScoped<ICountryRepository, CountryRepository>();
services.AddScoped<ICardRepository, CardRepository>();
services.AddScoped<ITransactionRepository, TransactionRepository>();

// CSV
services.AddScoped<ICsvReader<TopUpCsvRow>, TopUpCsvReader>();

// Business
services.AddScoped<IImportService, ImportService>();

var serviceProvider = services.BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

db.Database.EnsureCreated();
Console.WriteLine("Database created successfully.");

// ================= Import =================
if (File.Exists(csvPath))
{
    var importer = scope.ServiceProvider.GetRequiredService<IImportService>();
    await importer.ImportTopUpsAsync(csvPath);
    Console.WriteLine("Import completed.");
}
else
{
    Console.WriteLine("Import skipped (CSV not found).");
}

// ================= Counts =================
Console.WriteLine($"Users: {db.UserAccounts.Count()}");
Console.WriteLine($"Cards: {db.Cards.Count()}");
Console.WriteLine($"Countries: {db.Countries.Count()}");
Console.WriteLine($"Transactions: {db.Transactions.Count()}");
