using System;

namespace PayPalLab2.DataAccess.Csv;

public class TopUpCsvRow
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public string CardToken { get; set; } = null!;
    public string MaskedPan { get; set; } = null!;
    public string Expiry { get; set; } = null!;
    public string Brand { get; set; } = null!;

    public string IssuerCountryCode { get; set; } = null!;
    public string IssuerCountryName { get; set; } = null!;

    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string TxExternalId { get; set; } = null!;
}
