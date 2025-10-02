using AlcoholCalculator.Shared.Models;
using System.Text.Json;

namespace AlcoholCalculator.Shared.Services;

public class DrinkService : IDrinkService
{
    private List<Drink> _drinks = new();
    private readonly string _dataFile = "drinks.json";
    private readonly IStorageService _storageService;
    private bool _isInitialized = false;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public DrinkService(IStorageService storageService)
    {
        _storageService = storageService;
        InitializeSampleData();
    }

    private void InitializeSampleData()
    {
        if (_drinks.Any()) return;

        _drinks = new List<Drink>
        {
            new() {
                Id = 1,
                PersianName = "ویسکی",
                EnglishName = "Whiskey",
                Category = DrinkCategory.Whiskey,
                ShotVolume = 44,
                ShotInterval = 30,
                AlcoholPercentage = 40,
                SuitableFoods = "پنیر، گوشت قرمز، شکلات تلخ",
                AvoidFoods = "غذاهای شیرین، ماهی"
            },
            new() {
                Id = 2,
                PersianName = "ودکا",
                EnglishName = "Vodka",
                Category = DrinkCategory.Vodka,
                ShotVolume = 44,
                ShotInterval = 25,
                AlcoholPercentage = 40,
                SuitableFoods = "خاویار، زیتون، لیمو",
                AvoidFoods = "شیر، غذاهای چرب"
            },
            new() {
                Id = 3,
                PersianName = "آبجو",
                EnglishName = "Beer",
                Category = DrinkCategory.Beer,
                ShotVolume = 355,
                ShotInterval = 20,
                AlcoholPercentage = 5,
                SuitableFoods = "پیتزا، همبرگر، سیب زمینی سرخ کرده",
                AvoidFoods = "شیرینی، دسرهای شیرین"
            },
            new() {
                Id = 4,
                PersianName = "شراب قرمز",
                EnglishName = "Red Wine",
                Category = DrinkCategory.Wine,
                ShotVolume = 148,
                ShotInterval = 45,
                AlcoholPercentage = 12,
                SuitableFoods = "پاستا، استیک، پنیر",
                AvoidFoods = "غذاهای تند، شکلات شیری"
            }
        };
    }

    public async Task<List<Drink>> GetDrinksAsync()
    {
        await InitializeAsync();
        return await Task.FromResult(_drinks);
    }

    public async Task<Drink?> GetDrinkAsync(int id)
    {
        await InitializeAsync();
        return await Task.FromResult(_drinks.FirstOrDefault(d => d.Id == id));
    }

    public async Task AddDrinkAsync(Drink drink)
    {
        await _semaphore.WaitAsync();
        try
        {
            await InitializeAsync();
            drink.Id = _drinks.Any() ? _drinks.Max(d => d.Id) + 1 : 1;
            _drinks.Add(drink);
            await SaveDrinksAsync();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task UpdateDrinkAsync(Drink drink)
    {
        await _semaphore.WaitAsync();
        try
        {
            await InitializeAsync();
            var existing = _drinks.FirstOrDefault(d => d.Id == drink.Id);
            if (existing != null)
            {
                _drinks.Remove(existing);
                _drinks.Add(drink);
                await SaveDrinksAsync();
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task DeleteDrinkAsync(int id)
    {
        await _semaphore.WaitAsync();
        try
        {
            await InitializeAsync();
            var drink = _drinks.FirstOrDefault(d => d.Id == id);
            if (drink != null)
            {
                _drinks.Remove(drink);
                await SaveDrinksAsync();
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task InitializeAsync()
    {
        if (_isInitialized) return;

        try
        {
            // سعی در بارگذاری از storage
            await LoadDrinksFromStorageAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading drinks: {ex.Message}");
            // اگر بارگذاری از storage失敗 شد، از داده‌های نمونه استفاده کن
            InitializeSampleData();
        }

        _isInitialized = true;
    }

    private async Task LoadDrinksFromStorageAsync()
    {
        try
        {
            var json = await _storageService.ReadFileAsync(_dataFile);
            if (!string.IsNullOrEmpty(json))
            {
                var loadedDrinks = JsonSerializer.Deserialize<List<Drink>>(json);
                if (loadedDrinks != null && loadedDrinks.Any())
                {
                    _drinks = loadedDrinks;
                    return;
                }
            }

            // اگر فایل خالی بود یا وجود نداشت، از داده‌های نمونه استفاده کن
            InitializeSampleData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in LoadDrinksFromStorage: {ex.Message}");
            // در صورت بروز هر خطا، از داده‌های نمونه استفاده کن
            InitializeSampleData();
        }
    }

    private async Task SaveDrinksAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_drinks, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await _storageService.WriteFileAsync(_dataFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving drinks: {ex.Message}");
            // اگر ذخیره سازی失敗 شد، فقط در حافظه نگه دار
        }
    }
}