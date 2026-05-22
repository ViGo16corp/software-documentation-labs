namespace PayPalLab2.Business.Interfaces;

public interface IImportService
{
    Task ImportTopUpsAsync(string csvPath);
}
