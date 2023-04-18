using DataAccess.Interface;
using Domain.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceRepository _service;

    public ServiceController(IServiceRepository service)
    {
        _service = service;
    }

    #region ADD

    [HttpPost("addService")]
    public async Task<Service[]> AddService([FromForm] Service service)
    {
        return await _service.AddService(service);
    }

    [HttpPost("addCW")]
    public async Task<CategoryWork[]> AddCW([FromForm] CategoryWork categoryWork)
    {
        return await _service.AddCategoryWork(categoryWork);
    }

    [HttpPost("addTE")]
    public async Task<TypeEquipment[]> AddTE([FromForm] TypeEquipment type)
    {
        return await _service.AddTypeEquipment(type);
    }

    #endregion

    #region Update

    [HttpPost("updateWork")]
    public async Task<Work> UpdateWork([FromForm] Work work)
    {
        return await _service.UpdateWork(work);
    }

    [HttpPost("updateEquipment")]
    public async Task<Equipment> UpdateEquipment([FromForm] Equipment equipment)
    {
        return await _service.UpdateEquipment(equipment);
    }

    #endregion

    #region Get

    [HttpGet("getWork/\'{name}\'")]
    public async Task<Work> GetWork([FromRoute] string name)
    {
        return await _service.GetWork(name);
    }

    [HttpGet("getEquipment/{id:int}")]
    public async Task<Equipment> GetEquipment([FromRoute] int id)
    {
        return await _service.GetEquipment(id);
    }

    [HttpGet("getService")]
    public async Task<Service[]> GetServices()
    {
        return await _service.GetServices();
    }

    [HttpGet("getCategoryWorks")]
    public async Task<CategoryWork[]> GetCategoryWorks()
    {
        return await _service.GetCategoryWork();
    }

    [HttpGet("getTypeEquipment")]
    public async Task<TypeEquipment[]> GetTypeEquipment()
    {
        return await _service.GetTypeEquipments();
    }

    [HttpGet("getWorks")]
    public async Task<Work[]> GetWorks()
    {
        return await _service.GetWorks();
    }

    [HttpGet("getEquipments")]
    public async Task<Equipment[]> GetEquipments()
    {
        return await _service.GetEquipments();
    }

    [HttpGet("searchWorkFromCity/\'{nameCity}\'")]
    public async Task<Work[]> GetWorkFromCity([FromRoute] string nameCity)
    {
        return await _service.SearchWorkFromCity(nameCity);
    }

    [HttpGet("searchEquipmentFromCity/\'{nameCity}\'")]
    public async Task<Equipment[]> GetEquipmentFromCity([FromRoute] string nameCity)
    {
        return await _service.SearchEquipmentFromCity(nameCity);
    }

    #endregion
}