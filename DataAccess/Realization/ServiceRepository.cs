using DataAccess.Interface;
using Domain.Models.Service;

namespace DataAccess.Realization;

public class ServiceRepository : IServiceRepository
{
    private readonly DaraSqlService _service;

    public ServiceRepository(DaraSqlService service)
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

    public async Task<Work> AddWork(Work work)
    {
        return await _service.AddWork(work);
    }

    public async Task<TypeEquipment[]> AddTypeEquipment(TypeEquipment equipment)
    {
        return await _service.AddTypeEquipment(equipment);
    }

    public async Task<Equipment> AddEquipment(Equipment equipment)
    {
        return await _service.AddEquipment(equipment);
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
        return new Work();
    }

    public Task<Equipment> GetEquipment(int equipmentId)
    {
        throw new NotImplementedException();
    }

    public Task<Service[]> GetServices()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryWork[]> GetCategoryWork()
    {
        throw new NotImplementedException();
    }

    public Task<TypeEquipment[]> GetTypeEquipments()
    {
        throw new NotImplementedException();
    }

    public Task<Work[]> GetWorks()
    {
        throw new NotImplementedException();
    }

    public Task<Equipment[]> GetEquipments()
    {
        throw new NotImplementedException();
    }

    public Task<Work[]> SearchWorkFromCity(string nameCity)
    {
        throw new NotImplementedException();
    }

    public Task<Equipment[]> SearchWorkFromEquipment(string nameCity)
    {
        throw new NotImplementedException();
    }
}