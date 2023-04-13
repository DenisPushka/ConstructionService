using DataAccess.Interface;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class ServiceRepository : IServiceRepository
{
    private readonly DataSqlService _service;

    public ServiceRepository(DataSqlService service)
    {
        _service = service;
    }

    public async Task<Service[]> AddService(Service service)
    {
        return await _service.AddService(service);
    }

    public async Task<CategoryWork[]> AddCategoryWork(CategoryWork categoryWork)
    {
        return await _service.AddCategoryWork(categoryWork);
    }

    public async Task<Work> AddWork(Work work, Company company, Handcraft handcraft)
    {
        return await _service.AddWork(work, company, handcraft);
    }

    public async Task<TypeEquipment[]> AddTypeEquipment(TypeEquipment equipment)
    {
        return await _service.AddTypeEquipment(equipment);
    }

    public async Task<Equipment> AddEquipment(Equipment equipment,Company company, Handcraft handcraft)
    {
        return await _service.AddEquipment(equipment, company, handcraft);
    }

    public async Task<Work> UpdateWork(Work work)
    {
        return await _service.UpdateInfoAboutWork(work);
    }

    public async Task<Equipment> UpdateEquipment(Equipment equipment)
    {
        return await _service.UpdateInfoAboutEquipment(equipment);
    }

    public async Task<Work> GetWork(string nameWork)
    {
        return await _service.GetWork(nameWork);
    }

    public async Task<Equipment> GetEquipment(int equipmentId)
    {
        return await _service.GetEquipment(equipmentId);
    }

    public async Task<Service[]> GetServices()
    {
        return await _service.GetServices();
    }

    public async Task<CategoryWork[]> GetCategoryWork()
    {
        return await _service.GetCategoriesWork();
    }

    public async Task<TypeEquipment[]> GetTypeEquipments()
    {
        return await _service.GetTypeEquipments();
    }

    public async Task<Work[]> GetWorks()
    {
        return await _service.GetWorks();
    }

    public async Task<Equipment[]> GetEquipments()
    {
        return await _service.GetEquipments();
    }

    public async Task<Work[]> SearchWorkFromCity(string nameCity)
    {
        throw new NotImplementedException();
    }

    public async Task<Equipment[]> SearchEquipmentFromCity(string nameCity)
    {
        throw new NotImplementedException();
    }
}