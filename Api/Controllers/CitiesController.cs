using DataAccess.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly ICityRepository _cityRepository;

    public CitiesController(ICityRepository cityRepository) => _cityRepository = cityRepository;

    [HttpPost]
    public async Task<ActionResult> AddCity([FromBody] City city)
    {
        var b = await _cityRepository.AddCity(city);
        return Ok(b); //new CreatedAtRouteResult("GetCity", new { id = cities.Id }, cities);
    }

    [HttpGet("GetAll")]
    public async Task<City[]> GetAllCity()
    {
        return await _cityRepository.GetAllCity();
    }
}