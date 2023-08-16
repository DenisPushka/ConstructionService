using DataAccess.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для городов.
/// </summary>
[ApiController, Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    /// <summary>
    /// Репозиторий городов.
    /// </summary>
    private readonly ICityRepository _cityRepository;

    /// <summary>
    /// Конструктор с 1 параметром.
    /// </summary>
    public CitiesController(ICityRepository cityRepository) => _cityRepository = cityRepository;

    /// <summary>
    /// Добавление города.
    /// </summary>
    /// <param name="city">Город.</param>
    [HttpPost]
    public async Task<ActionResult> AddCity([FromForm] City city)
    {
        return Ok(await _cityRepository.AddCity(city)); //new CreatedAtRouteResult("GetCity", new { id = cities.Id }, cities);
    }

    /// <summary>
    /// Получение всех городов.
    /// </summary>
    /// <returns>Массив городов.</returns>
    [HttpGet("GetAll")]
    public async Task<City[]> GetAllCity()
    {
        return await _cityRepository.GetAllCity();
    }
}