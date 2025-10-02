using AlcoholCalculator.Shared.Services;

namespace AlcoholCalculator.Shared.Models;

public class AlcoholCalculatorService : IAlcoholCalculatorService
{
    public CalculationResult CalculateIntoxication(CalculationRequest request)
    {
        var drink = request.SelectedDrink;
        var alcoholPercentage = drink?.AlcoholPercentage ?? request.CustomAlcoholPercentage;
        var shotVolume = drink?.ShotVolume ?? request.CustomShotVolume;
        var shotInterval = drink?.ShotInterval ?? request.CustomShotInterval;

        // محاسبات ساده‌شده برای نمونه
        double weightFactor = request.Weight / 70.0;
        double genderFactor = request.Gender.ToLower() == "female" ? 0.85 : 1.0;
        double ageFactor = Math.Max(0.7, 1.0 - (request.Age - 25) * 0.01);
        double foodFactor = GetFoodFactor(request.FoodLevel);

        double targetBAC = GetTargetBAC(request.TargetIntoxication);

        // محاسبه تعداد شات‌ها
        double alcoholGramsPerShot = (shotVolume * alcoholPercentage / 100) * 0.789;
        double metabolicRate = 0.015; // گرم الکل در دقیقه

        int minShots = (int)Math.Ceiling((targetBAC * 0.8 * weightFactor * 1000) /
                       (alcoholGramsPerShot * genderFactor * ageFactor * foodFactor));
        int maxShots = (int)Math.Ceiling((targetBAC * 1.2 * weightFactor * 1000) /
                       (alcoholGramsPerShot * genderFactor * ageFactor * foodFactor));

        minShots = Math.Max(1, minShots);
        maxShots = Math.Max(minShots, maxShots);

        var result = new CalculationResult
        {
            MinShots = minShots,
            MaxShots = maxShots,
            TotalVolume = (minShots + maxShots) / 2.0 * shotVolume,
            DrinkingDuration = (minShots + maxShots) / 2 * shotInterval,
            SoberingTime = (int)((targetBAC * 1000) / metabolicRate),
            RiskLevel = GetRiskLevel(request.TargetIntoxication),
            WarningMessage = GetWarningMessage(request.TargetIntoxication)
        };

        return result;
    }

    private double GetFoodFactor(FoodLevel foodLevel)
    {
        return foodLevel switch
        {
            FoodLevel.Empty => 1.0,
            FoodLevel.Light => 0.8,
            FoodLevel.Medium => 0.6,
            FoodLevel.Heavy => 0.4,
            _ => 1.0
        };
    }

    private double GetTargetBAC(IntoxicationLevel level)
    {
        return level switch
        {
            IntoxicationLevel.Sober => 0.0,
            IntoxicationLevel.Mild => 0.03,
            IntoxicationLevel.Euphoric => 0.06,
            IntoxicationLevel.Impaired => 0.10,
            IntoxicationLevel.Confused => 0.15,
            IntoxicationLevel.Stupor => 0.25,
            IntoxicationLevel.Coma => 0.35,
            IntoxicationLevel.Lethal => 0.45,
            _ => 0.0
        };
    }

    private string GetRiskLevel(IntoxicationLevel level)
    {
        return level switch
        {
            IntoxicationLevel.Sober => "بی‌خطر",
            IntoxicationLevel.Mild => "کم",
            IntoxicationLevel.Euphoric => "متوسط",
            IntoxicationLevel.Impaired => "بالا",
            IntoxicationLevel.Confused => "خیلی بالا",
            IntoxicationLevel.Stupor => "خطرناک",
            IntoxicationLevel.Coma => "بسیار خطرناک",
            IntoxicationLevel.Lethal => "کشنده",
            _ => "نامشخص"
        };
    }

    private string GetWarningMessage(IntoxicationLevel level)
    {
        return level switch
        {
            IntoxicationLevel.Impaired => "رانندگی ممنوع!",
            IntoxicationLevel.Confused => "نیاز به مراقبت دارد",
            IntoxicationLevel.Stupor => "فوراً به پزشک مراجعه کنید",
            IntoxicationLevel.Coma => "اورژانسی - با اورژانس تماس بگیرید",
            IntoxicationLevel.Lethal => "خطر مرگ - فوراً با اورژانس تماس بگیرید",
            _ => ""
        };
    }
}
