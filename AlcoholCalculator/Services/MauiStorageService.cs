using AlcoholCalculator.Shared.Services;

namespace AlcoholCalculator.Services;

public class MauiStorageService : IStorageService
{
    public async Task<string> ReadFileAsync(string fileName)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        if (File.Exists(filePath))
        {
            return await File.ReadAllTextAsync(filePath);
        }
        return string.Empty;
    }

    public async Task WriteFileAsync(string fileName, string content)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        await File.WriteAllTextAsync(filePath, content);
    }

    public Task<bool> FileExistsAsync(string fileName)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        return Task.FromResult(File.Exists(filePath));
    }
}
