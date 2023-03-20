using Domain.Models.Service;

namespace DataAccess.Interface;

public interface IServiceRepository
{
    // Добавление
    Task<Service[]> AddService(Service service);
    
    Task<CategoryWork[]> AddCategoryWork(CategoryWork categoryWork);
    Task<Work> AddWork(Work work);

    Task<TypeEquipment[]> AddTypeEquipment(TypeEquipment equipment);
    Task<Equipment> AddEquipment(Equipment equipment);
    
    // Обновление
    Task<Work> UpdateWork(Work work);
    Task<Equipment> UpdateEquipment(Equipment equipment);
    
    // Получение
    Task<Work> GetWork(string nameWork);
    Task<Equipment> GetEquipment(int equipmentId);
    Task<Service[]> GetServices();
    Task<CategoryWork[]> GetCategoryWork();
    Task<TypeEquipment[]> GetTypeEquipments();
    Task<Work[]> GetWorks();
    Task<Equipment[]> GetEquipments();
    
    // Со специальными параметрами
    Task<Work[]> SearchWorkFromCity(string nameCity);
    Task<Equipment[]> SearchWorkFromEquipment(string nameCity);
    
}