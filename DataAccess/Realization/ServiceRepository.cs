using System;
using System.Threading.Tasks;
using DataAccess.Interface;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Realization;

/// <summary>
/// Репозиторий для свервиса.
/// </summary>
/// Категорий работы (КР),
/// Тип оборудования (ТО).
public class ServiceRepository : IServiceRepository
{
    /// <summary>
    /// Реализатор для сервиса.
    /// </summary>
    private readonly DataSqlService _service;

    public ServiceRepository(DataSqlService service)
    {
        _service = service;
    }

    /// <summary>
    /// Добавление сервиса.
    /// </summary>
    /// <param name="service">Добавляемый сервис.</param>
    /// <returns>Массив сервисов.</returns>
    public async Task<Service[]> AddService(Service service)
    {
        return await _service.AddService(service);
    }

    /// <summary>
    /// Добавление КР.
    /// </summary>
    /// <param name="categoryWork">КР.</param>
    /// <returns>Массив КР.</returns>
    public async Task<CategoryWork[]> AddCategoryWork(CategoryWork categoryWork)
    {
        return await _service.AddCategoryWork(categoryWork);
    }

    /// <summary>
    /// Добавление (создание) работы.
    /// </summary>
    /// <param name="work">Работа.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns>Работа.</returns>
    public async Task<Work> AddWork(Work work, Company company, Handcraft handcraft)
    {
        return await _service.AddWork(work, company, handcraft);
    }

    /// <summary>
    /// Добавление ТО.
    /// </summary>
    /// <param name="equipment">ТО.</param>
    /// <returns>Массив ТО.</returns>
    public async Task<TypeEquipment[]> AddTypeEquipment(TypeEquipment equipment)
    {
        return await _service.AddTypeEquipment(equipment);
    }

    /// <summary>
    /// Добавление оборудования.
    /// </summary>
    /// <param name="equipment">Оборудование.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns></returns>
    public async Task<Equipment> AddEquipment(Equipment equipment, Company company, Handcraft handcraft)
    {
        return await _service.AddEquipment(equipment, company, handcraft);
    }

    /// <summary>
    /// Обновление работы.
    /// </summary>
    /// <param name="work">Обновленная работа.</param>
    /// <returns>Обновленная работа.</returns>
    public async Task<Work> UpdateWork(Work work)
    {
        return await _service.UpdateInfoAboutWork(work);
    }

    /// <summary>
    /// Обновление оборудования.
    /// </summary>
    /// <param name="equipment">Обновленное оборудование.</param>
    /// <returns>Обновленное оборудование.</returns>
    public async Task<Equipment> UpdateEquipment(Equipment equipment)
    {
        return await _service.UpdateInfoAboutEquipment(equipment);
    }

    /// <summary>
    /// Получение работы.
    /// </summary>
    /// <param name="nameWork">Название работы.</param>
    /// <returns>Работа.</returns>
    public async Task<Work> GetWork(string nameWork)
    {
        return await _service.GetWork(nameWork);
    }

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Оборудование.</returns>
    public async Task<Equipment> GetEquipment(int equipmentId)
    {
        return await _service.GetEquipment(equipmentId);
    }

    /// <summary>
    /// Получение сервисов.
    /// </summary>
    /// <returns>Массив сервисов.</returns>
    public async Task<Service[]> GetServices()
    {
        return await _service.GetServices();
    }

    /// <summary>
    /// Получение КР.
    /// </summary>
    /// <returns>Массив КР.</returns>
    public async Task<CategoryWork[]> GetCategoryWork()
    {
        return await _service.GetCategoriesWork();
    }

    /// <summary>
    /// Получение ТО.
    /// </summary>
    /// <returns>Массив ТО.</returns>
    public async Task<TypeEquipment[]> GetTypeEquipments()
    {
        return await _service.GetTypeEquipments();
    }

    /// <summary>
    /// Получение работ.
    /// </summary>
    /// <returns>Массив работ.</returns>
    public async Task<Work[]> GetWorks()
    {
        return await _service.GetWorks();
    }

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <returns>Массив оборудований.</returns>
    public async Task<Equipment[]> GetEquipments()
    {
        return await _service.GetEquipments();
    }

    /// <summary>
    /// Получение работ из <paramref name="nameCity"/>.
    /// </summary>
    /// <param name="nameCity">Город, в котром ведется поиск.</param>
    /// <returns>Массив работ.</returns>
    public async Task<Work[]> SearchWorkFromCity(string nameCity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Получение оборудования из <paramref name="nameCity"/>.
    /// </summary>
    /// <param name="nameCity">Город, в котром ведется поиск.</param>
    /// <returns>Массив оборудований.</returns>
    public async Task<Equipment[]> SearchEquipmentFromCity(string nameCity)
    {
        throw new NotImplementedException();
    }
}