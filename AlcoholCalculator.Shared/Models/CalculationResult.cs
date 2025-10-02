namespace AlcoholCalculator.Shared.Models;

public class CalculationResult
{
    public int MinShots { get; set; }
    public int MaxShots { get; set; }
    public double TotalVolume { get; set; } // میلی‌لیتر
    public int DrinkingDuration { get; set; } // دقیقه
    public int SoberingTime { get; set; } // دقیقه
    public string RiskLevel { get; set; } = string.Empty;
    public string WarningMessage { get; set; } = string.Empty;
}