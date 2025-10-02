using System.ComponentModel.DataAnnotations;

namespace AlcoholCalculator.Shared.Models;

public class Drink
{
    public int Id { get; set; }

    [Required(ErrorMessage = "نام فارسی الزامی است")]
    [StringLength(100, ErrorMessage = "نام فارسی نمی‌تواند بیش از 100 کاراکتر باشد")]
    public string PersianName { get; set; } = string.Empty;

    [Required(ErrorMessage = "نام انگلیسی الزامی است")]
    [StringLength(100, ErrorMessage = "نام انگلیسی نمی‌تواند بیش از 100 کاراکتر باشد")]
    public string EnglishName { get; set; } = string.Empty;

    [Required(ErrorMessage = "دسته بندی الزامی است")]
    public DrinkCategory Category { get; set; }

    [Required(ErrorMessage = "حجم شات الزامی است")]
    [Range(1, 1000, ErrorMessage = "حجم شات باید بین 1 تا 1000 میلی‌لیتر باشد")]
    public double ShotVolume { get; set; }

    [Required(ErrorMessage = "فاصله شات‌ها الزامی است")]
    [Range(1, 300, ErrorMessage = "فاصله شات‌ها باید بین 1 تا 300 دقیقه باشد")]
    public int ShotInterval { get; set; }

    [Required(ErrorMessage = "درصد الکل الزامی است")]
    [Range(0.1, 100, ErrorMessage = "درصد الکل باید بین 0.1 تا 100 باشد")]
    public double AlcoholPercentage { get; set; }

    [StringLength(500, ErrorMessage = "غذاهای مناسب نمی‌تواند بیش از 500 کاراکتر باشد")]
    public string SuitableFoods { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "غذاهای ممنوعه نمی‌تواند بیش از 500 کاراکتر باشد")]
    public string AvoidFoods { get; set; } = string.Empty;
}