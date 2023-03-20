using Domain.Models;

namespace DataAccess.Interface;

public interface ICityRepository
{
    Task<bool> AddCity(City city);
    
    Task<City[]> GetAllCity();
}