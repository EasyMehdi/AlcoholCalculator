using AlcoholCalculator.Shared.Models;

namespace AlcoholCalculator.Shared.Services;

public interface IAlcoholCalculatorService
{
    CalculationResult CalculateIntoxication(CalculationRequest request);
}