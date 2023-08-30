using System.Threading.Tasks;
using DataAccess.Interface;
using Domain.Models.Service;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер для сервиса. 
/// </summary>
/// КР (категорий работ),
/// ТО (тип оборудования).
[ApiController, Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    /// <summary>
    /// Репозиторий для сервиса.
    /// </summary>
    private readonly IServiceRepository _service;

    public ServiceController(IServiceRepository service)
    {
        _service = service;
    }

    #region ADD

    /// <summary>
    /// Добавление сервиса.
    /// </summary>
    /// <param name="service">Сервис.</param>
    /// <returns>Массив сервисов.</returns>
    [HttpPost("addService")]
    public async Task<Service[]> AddService([FromForm] Service service)
    {
        return await _service.AddService(service);
    }

    /// <summary>
    /// Добавление категории работ.
    /// </summary>
    /// <param name="categoryWork">Категория работ.</param>
    /// <returns>Массив категорий работ.</returns>
    [HttpPost("addCW")]
    public async Task<CategoryWork[]> AddCW([FromForm] CategoryWork categoryWork)
    {
        return await _service.AddCategoryWork(categoryWork);
    }

    /// <summary>
    /// Добавление типа оборудования.
    /// </summary>
    /// <param name="type">Тип оборудования.</param>
    /// <returns>Массив типов оборудования.</returns>
    [HttpPost("addTE")]
    public async Task<TypeEquipment[]> AddTE([FromForm] TypeEquipment type)
    {
        return await _service.AddTypeEquipment(type);
    }

    #endregion

    #region Update

    /// <summary>
    /// Обновление работы.
    /// </summary>
    /// <param name="work">Обновленная работа.</param>
    /// <returns>Обновленная работа.</returns>
    [HttpPost("updateWork")]
    public async Task<Work> UpdateWork([FromForm] Work work)
    {
        return await _service.UpdateWork(work);
    }

    /// <summary>
    /// Обновление оборудования.
    /// </summary>
    /// <param name="equipment">Обновляемое оборудование.</param>
    /// <returns>Обновляемое оборудование.</returns>
    [HttpPost("updateEquipment")]
    public async Task<Equipment> UpdateEquipment([FromForm] Equipment equipment)
    {
        return await _service.UpdateEquipment(equipment);
    }

    #endregion

    #region Get

    /// <summary>
    /// Получение работы.
    /// </summary>
    /// <param name="name">Название работы.</param>
    /// <returns>Работа.</returns>
    [HttpGet("getWork/\'{name}\'")]
    public async Task<Work> GetWork([FromRoute] string name)
    {
        return await _service.GetWork(name);
    }

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <param name="id">Id оборудования.</param>
    /// <returns>Оборудование.</returns>
    [HttpGet("getEquipment/{id:int}")]
    public async Task<Equipment> GetEquipment([FromRoute] int id)
    {
        return await _service.GetEquipment(id);
    }

    /// <summary>
    /// Получение сервисов.
    /// </summary>
    /// <returns>Массив сервисов.</returns>
    [HttpGet("getService")]
    public async Task<Service[]> GetServices()
    {
        return await _service.GetServices();
    }

    /// <summary>
    /// Получение категорий работ.
    /// </summary>
    /// <returns>Массив КТ.</returns>
    [HttpGet("getCategoryWorks")]
    public async Task<CategoryWork[]> GetCategoryWorks()
    {
        return await _service.GetCategoryWork();
    }

    /// <summary>
    /// Получение ТО.
    /// </summary>
    /// <returns>Массив ТО.</returns>
    [HttpGet("getTypeEquipment")]
    public async Task<TypeEquipment[]> GetTypeEquipment()
    {
        return await _service.GetTypeEquipments();
    }

    /// <summary>
    /// Получение работ.
    /// </summary>
    /// <returns>Массив работ.</returns>
    [HttpGet("getWorks")]
    public async Task<Work[]> GetWorks()
    {
        return await _service.GetWorks();
    }

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <returns>Массив оборудования.</returns>
    [HttpGet("getEquipments")]
    public async Task<Equipment[]> GetEquipments()
    {
        return await _service.GetEquipments();
    }

    /// <summary>
    /// Получение работ из <paramref name="nameCity"/>.
    /// </summary>
    /// <param name="nameCity">Город, по которому идет поиск.</param>
    /// <returns>Массив работ.</returns>
    [HttpGet("searchWorkFromCity/\'{nameCity}\'")]
    public async Task<Work[]> GetWorkFromCity([FromRoute] string nameCity)
    {
        return await _service.SearchWorkFromCity(nameCity);
    }

    /// <summary>
    /// Получение оборудования из <paramref name="nameCity"/>.
    /// </summary>
    /// <param name="nameCity">Город, по которому идет поиск.</param>
    /// <returns>Массив оборудований.</returns>
    [HttpGet("searchEquipmentFromCity/\'{nameCity}\'")]
    public async Task<Equipment[]> GetEquipmentFromCity([FromRoute] string nameCity)
    {
        return await _service.SearchEquipmentFromCity(nameCity);
    }

    #endregion
}