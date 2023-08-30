using System.Threading.Tasks;
using Domain.Models;

namespace DataAccess.Interface;

/// <summary>
/// Интерфейс репозитория города.
/// </summary>
public interface ICityRepository
{
    /// <summary>
    /// Добавление города.
    /// </summary>
    /// <param name="city">Добавляемый город</param>
    /// <returns>true - в случае успеха.</returns>
    Task<bool> AddCity(City city);
    
    /// <summary>
    /// Получение всех городов.
    /// </summary>
    /// <returns>Массив городов.</returns>
    Task<City[]> GetAllCity();
}