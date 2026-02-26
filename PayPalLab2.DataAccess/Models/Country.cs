using System.Collections.Generic;

namespace PayPalLab2.DataAccess.Models;

public class Country
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    public List<Card> Cards { get; set; } = new();
}
