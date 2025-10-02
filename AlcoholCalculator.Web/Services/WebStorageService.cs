using AlcoholCalculator.Shared.Services;
using Microsoft.JSInterop;

namespace AlcoholCalculator.Web.Services;

public class WebStorageService : IStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public WebStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string> ReadFileAsync(string fileName)
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", fileName);
        }
        catch
        {
            return string.Empty;
        }
    }

    public async Task WriteFileAsync(string fileName, string content)
    {
        try
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", fileName, content);
        }
        catch
        {
            // ignore errors
        }
    }

    public async Task<bool> FileExistsAsync(string fileName)
    {
        try
        {
            var content = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", fileName);
            return !string.IsNullOrEmpty(content);
        }
        catch
        {
            return false;
        }
    }
}
