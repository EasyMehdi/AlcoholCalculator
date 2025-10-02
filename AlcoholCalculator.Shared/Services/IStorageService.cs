namespace AlcoholCalculator.Shared.Services;

public interface IStorageService
{
    Task<string> ReadFileAsync(string fileName);
    Task WriteFileAsync(string fileName, string content);
    Task<bool> FileExistsAsync(string fileName);
}