using AlcoholCalculator.Services;
using AlcoholCalculator.Shared.Services;
using Microsoft.Extensions.Logging;
using AlcoholCalculator.Shared.Models;

namespace AlcoholCalculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the AlcoholCalculator.Shared project
            builder.Services.AddSingleton<IStorageService, MauiStorageService>();
            builder.Services.AddSingleton<IDrinkService, DrinkService>();
            builder.Services.AddSingleton<IAlcoholCalculatorService, AlcoholCalculatorService>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
