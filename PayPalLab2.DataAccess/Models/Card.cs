namespace PayPalLab2.DataAccess.Models;

public class Card : PaymentMethod
{
    public string MaskedPan { get; set; } = null!;
    public string Expiry { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Token { get; set; } = null!;

    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
}
