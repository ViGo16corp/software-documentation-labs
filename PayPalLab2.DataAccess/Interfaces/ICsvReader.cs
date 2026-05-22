namespace PayPalLab2.DataAccess.Interfaces;

public interface ICsvReader<T>
{
    IEnumerable<T> Read(string filePath);
}
