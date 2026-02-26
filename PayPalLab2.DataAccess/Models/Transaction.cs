using System;

namespace PayPalLab2.DataAccess.Models;

public class Transaction
{
    public int Id { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public TxStatus Status { get; set; }

    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; } = null!;

    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; } = null!;
}

public enum TxStatus
{
    Created,
    Success,
    Failed
}
