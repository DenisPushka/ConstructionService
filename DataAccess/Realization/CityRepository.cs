using DataAccess.Interface;
using Domain.Models;

namespace DataAccess.Realization;

/// <summary>
/// Репозиторий города.
/// </summary>
public class CityRepository : ICityRepository
{
    /// <summary>
    /// Реализатор.
    /// </summary>
    private readonly DataSql _context;

    public CityRepository(DataSql context) => _context = context;

    /// <summary>
    /// Добавление города.
    /// </summary>
    /// <param name="city">Город.</param>
    /// <returns>true - в случае успеха.</returns>
    public async Task<bool> AddCity(City city) => await _context.AddCity(city);

    /// <summary>
    /// Получние всех городов.
    /// </summary>
    /// <returns>Массив городов.</returns>
    public async Task<City[]> GetAllCity() => await _context.GetAllCity();
}