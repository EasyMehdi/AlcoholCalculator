using AlcoholCalculator.Shared.Models;

namespace AlcoholCalculator.Shared.Services;

public interface IDrinkService
{
    /// <summary>
    /// دریافت لیست تمام نوشیدنی‌ها
    /// </summary>
    /// <returns>لیست نوشیدنی‌ها</returns>
    Task<List<Drink>> GetDrinksAsync();

    /// <summary>
    /// دریافت یک نوشیدنی بر اساس شناسه
    /// </summary>
    /// <param name="id">شناسه نوشیدنی</param>
    /// <returns>نوشیدنی مورد نظر یا null اگر پیدا نشد</returns>
    Task<Drink?> GetDrinkAsync(int id);

    /// <summary>
    /// افزودن نوشیدنی جدید
    /// </summary>
    /// <param name="drink">نوشیدنی جدید</param>
    Task AddDrinkAsync(Drink drink);

    /// <summary>
    /// به‌روزرسانی نوشیدنی موجود
    /// </summary>
    /// <param name="drink">نوشیدنی با اطلاعات به‌روز شده</param>
    Task UpdateDrinkAsync(Drink drink);

    /// <summary>
    /// حذف نوشیدنی بر اساس شناسه
    /// </summary>
    /// <param name="id">شناسه نوشیدنی برای حذف</param>
    Task DeleteDrinkAsync(int id);
}