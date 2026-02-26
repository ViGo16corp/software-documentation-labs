using System.Collections.Generic;

namespace PayPalLab2.DataAccess.Models;

public class UserAccount
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public AccountStatus Status { get; set; }

    public Profile Profile { get; set; } = null!;
    public List<PaymentMethod> PaymentMethods { get; set; } = new();
    public List<Transaction> Transactions { get; set; } = new();
}

public enum AccountStatus
{
    Pending,
    Active,
    Blocked
}
