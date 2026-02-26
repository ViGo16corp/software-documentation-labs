using System.Globalization;
using System.Text;

static string RandomDigits(Random rnd, int count)
{
    var sb = new StringBuilder(count);
    for (int i = 0; i < count; i++) sb.Append(rnd.Next(0, 10));
    return sb.ToString();
}

static string MaskPan(Random rnd)
{
    var first4 = RandomDigits(rnd, 4);
    var last4 = RandomDigits(rnd, 4);
    return $"{first4}********{last4}";
}

static string RandomExpiry(Random rnd)
{
    var month = rnd.Next(1, 13);
    var year = rnd.Next(26, 35);
    return $"{month:00}/{year:00}";
}

static string RandomBrand(Random rnd) =>
    new[] { "VISA", "MASTERCARD", "AMEX" }[rnd.Next(0, 3)];

static (string Code, string Name) RandomCountry(Random rnd)
{
    var countries = new (string Code, string Name)[]
    {
        ("UA","Ukraine"),
        ("PL","Poland"),
        ("DE","Germany"),
        ("FR","France"),
        ("IT","Italy"),
        ("ES","Spain"),
        ("US","United States"),
        ("GB","United Kingdom"),
        ("CZ","Czechia"),
        ("RO","Romania")
    };
    return countries[rnd.Next(0, countries.Length)];
}

static string RandomPhone(Random rnd) => $"+380{rnd.Next(50, 100)}{RandomDigits(rnd, 7)}";

static string RandomName(Random rnd)
{
    var first = new[] { "Vika", "Anna", "Olena", "Maria", "Sofia", "Daria", "Iryna", "Kateryna" };
    var last = new[] { "Koval", "Shevchenko", "Bondarenko", "Melnyk", "Tkachenko", "Kravchenko" };
    return $"{first[rnd.Next(first.Length)]} {last[rnd.Next(last.Length)]}";
}

static string RandomEmail(Random rnd, int userIndex)
{
    return $"user{userIndex:D4}@example.com";
}

static string RandomToken(int cardIndex)
{
    return $"tok_{cardIndex:D6}";
}


var outPath = args.Length >= 1 ? args[0] : "topups.csv";
var rows = args.Length >= 2 && int.TryParse(args[1], out var n) ? n : 1000;

if (rows < 1000)
{
    Console.WriteLine("Rows must be >= 1000. Using 1000.");
    rows = 1000;
}

var rnd = new Random();
int usersCount = 50;
int cardsCount = 120;

using var writer = new StreamWriter(outPath, false, Encoding.UTF8);

writer.WriteLine("email,fullName,phone,cardToken,maskedPan,expiry,brand,issuerCountryCode,issuerCountryName,amount,createdAt,txExternalId");

var start = DateTime.UtcNow.AddDays(-30);

for (int i = 0; i < rows; i++)
{
    int userIndex = rnd.Next(0, usersCount);
    int cardIndex = rnd.Next(0, cardsCount);

    var email = RandomEmail(rnd, userIndex);
    var fullName = RandomName(rnd);
    var phone = RandomPhone(rnd);

    var cardToken = RandomToken(cardIndex);
    var maskedPan = MaskPan(rnd);
    var expiry = RandomExpiry(rnd);
    var brand = RandomBrand(rnd);

    var (cc, cn) = RandomCountry(rnd);

    var amount = Math.Round(rnd.NextDouble() * 490 + 10, 2);
    var createdAt = start.AddMinutes(rnd.Next(0, 60 * 24 * 30));
    var txExternalId = $"tx_{Guid.NewGuid():N}";

    writer.WriteLine(string.Join(",",
        email,
        fullName,
        phone,
        cardToken,
        maskedPan,
        expiry,
        brand,
        cc,
        cn,
        amount.ToString(CultureInfo.InvariantCulture),
        createdAt.ToString("O", CultureInfo.InvariantCulture),
        txExternalId
    ));
}

Console.WriteLine($"Generated {rows} rows -> {outPath}");
