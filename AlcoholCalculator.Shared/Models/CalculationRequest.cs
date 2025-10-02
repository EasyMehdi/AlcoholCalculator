namespace AlcoholCalculator.Shared.Models;

public class CalculationRequest
{
    public Drink? SelectedDrink { get; set; }
    public double Weight { get; set; } // کیلوگرم
    public int Age { get; set; }
    public string Gender { get; set; } = "male"; // male/female
    public IntoxicationLevel TargetIntoxication { get; set; }
    public FoodLevel FoodLevel { get; set; }
    public double CustomAlcoholPercentage { get; set; }
    public double CustomShotVolume { get; set; }
    public int CustomShotInterval { get; set; }
}