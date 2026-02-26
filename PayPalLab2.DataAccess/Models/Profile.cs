namespace PayPalLab2.DataAccess.Models;

public class Profile
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; } = null!;
}
