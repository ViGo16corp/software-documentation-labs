using System.Globalization;
using PayPalLab2.DataAccess.Interfaces;

namespace PayPalLab2.DataAccess.Csv;

public class TopUpCsvReader : ICsvReader<TopUpCsvRow>
{
    public IEnumerable<TopUpCsvRow> Read(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"CSV file not found: {filePath}");

        using var reader = new StreamReader(filePath);

        // header
        var header = reader.ReadLine();
        if (header is null)
            yield break;

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // простий split — для лабораторної ок, якщо без ком в середині полів
            var parts = line.Split(',');

           if (parts.Length < 12)
              continue;


            yield return new TopUpCsvRow
            {
                Email = parts[0].Trim(),
                FullName = parts[1].Trim(),
                Phone = parts[2].Trim(),

                CardToken = parts[3].Trim(),
                MaskedPan = parts[4].Trim(),
                Expiry = parts[5].Trim(),
                Brand = parts[6].Trim(),

                IssuerCountryCode = parts[7].Trim(),
                IssuerCountryName = parts[8].Trim(),

                Amount = double.Parse(parts[9].Trim(), CultureInfo.InvariantCulture),
                CreatedAt = DateTime.Parse(parts[10].Trim(), CultureInfo.InvariantCulture),
                TxExternalId = parts[11].Trim()
            };
        }
    }
}
