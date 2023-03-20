using DataAccess.Interface;
using Domain.Models;

namespace DataAccess.Realization;

public class CityRepository : ICityRepository
{
    private readonly DataSql _context;

    public CityRepository(DataSql context) => _context = context;

    public async Task<bool> AddCity(City city) => await _context.AddCity(city);

    public async Task<City[]> GetAllCity()
    {
        return await _context.GetAllCity();
    }
}