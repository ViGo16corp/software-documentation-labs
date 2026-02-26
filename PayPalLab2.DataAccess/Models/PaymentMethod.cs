namespace PayPalLab2.DataAccess.Models;

public abstract class PaymentMethod
{
    public int Id { get; set; }
    public bool IsDefault { get; set; }

    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; } = null!;
}
