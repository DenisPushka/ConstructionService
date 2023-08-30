using System.Threading.Tasks;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Interface;

/// <summary>
/// Интерфейс репозитория свервиса.
/// </summary>
/// Категорий работы (КР),
/// Тип оборудования (ТО).
public interface IServiceRepository
{
    // Добавление

    /// <summary>
    /// Добавление сервиса.
    /// </summary>
    /// <param name="service">Добавляемый сервис.</param>
    /// <returns>Массив сервисов.</returns>
    Task<Service[]> AddService(Service service);

    /// <summary>
    /// Добавление КР.
    /// </summary>
    /// <param name="categoryWork">КР.</param>
    /// <returns>Массив КР.</returns>
    Task<CategoryWork[]> AddCategoryWork(CategoryWork categoryWork);

    /// <summary>
    /// Добавление (создание) работы.
    /// </summary>
    /// <param name="work">Работа.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns>Работа.</returns>
    Task<Work> AddWork(Work work, Company company, Handcraft handcraft);

    /// <summary>
    /// Добавление ТО.
    /// </summary>
    /// <param name="equipment">ТО.</param>
    /// <returns>Массив ТО.</returns>
    Task<TypeEquipment[]> AddTypeEquipment(TypeEquipment equipment);

    /// <summary>
    /// Добавление оборудования.
    /// </summary>
    /// <param name="equipment">Оборудование.</param>
    /// <param name="company">Компания.</param>
    /// <param name="handcraft">Ремесленник.</param>
    /// <returns></returns>
    Task<Equipment> AddEquipment(Equipment equipment, Company company, Handcraft handcraft);
    
    //------------------------------------------------------------------------------------------------------------------
    // Обновление

    /// <summary>
    /// Обновление работы.
    /// </summary>
    /// <param name="work">Обновленная работа.</param>
    /// <returns>Обновленная работа.</returns>
    Task<Work> UpdateWork(Work work);

    /// <summary>
    /// Обновление оборудования.
    /// </summary>
    /// <param name="equipment">Обновленное оборудование.</param>
    /// <returns>Обновленное оборудование.</returns>
    Task<Equipment> UpdateEquipment(Equipment equipment);
    
    //------------------------------------------------------------------------------------------------------------------
    // Получение

    /// <summary>
    /// Получение работы.
    /// </summary>
    /// <param name="nameWork">Название работы.</param>
    /// <returns>Работа.</returns>
    Task<Work> GetWork(string nameWork);

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <param name="equipmentId">Id оборудования.</param>
    /// <returns>Оборудование.</returns>
    Task<Equipment> GetEquipment(int equipmentId);

    /// <summary>
    /// Получение сервисов.
    /// </summary>
    /// <returns>Массив сервисов.</returns>
    Task<Service[]> GetServices();

    /// <summary>
    /// Получение КР.
    /// </summary>
    /// <returns>Массив КР.</returns>
    Task<CategoryWork[]> GetCategoryWork();

    /// <summary>
    /// Получение ТО.
    /// </summary>
    /// <returns>Массив ТО.</returns>
    Task<TypeEquipment[]> GetTypeEquipments();

    /// <summary>
    /// Получение работ.
    /// </summary>
    /// <returns>Массив работ.</returns>
    Task<Work[]> GetWorks();

    /// <summary>
    /// Получение оборудования.
    /// </summary>
    /// <returns>Массив оборудований.</returns>
    Task<Equipment[]> GetEquipments();

    //------------------------------------------------------------------------------------------------------------------
    // Со специальными параметрами

    /// <summary>
    /// Получение работ из <paramref name="nameCity"/>.
    /// </summary>
    /// <param name="nameCity">Город, в котром ведется поиск.</param>
    /// <returns>Массив работ.</returns>
    Task<Work[]> SearchWorkFromCity(string nameCity);

    /// <summary>
    /// Получение оборудования из <paramref name="nameCity"/>.
    /// </summary>
    /// <param name="nameCity">Город, в котром ведется поиск.</param>
    /// <returns>Массив оборудований.</returns>
    Task<Equipment[]> SearchEquipmentFromCity(string nameCity);
}