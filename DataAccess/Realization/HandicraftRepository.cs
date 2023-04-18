using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class HandcraftRepository : IHandcraftRepository
{
    private DataSqlHandCraft _sqlHandCraft;

    public HandcraftRepository(DataSqlHandCraft sqlHandCraft)
    {
        _sqlHandCraft = sqlHandCraft;
    }

    public Task<Handcraft> GetHandcraft(UserAuthentication handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft[]> GetAllHandcrafts()
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft[]> GetHandcraftFromCity(string city)
    {
        throw new NotImplementedException();
    }

    public Task<Feedback[]> GetFeedbacks(Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Work[]> GetWorks(UserAuthentication handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Equipment[]> GetEquipments(UserAuthentication handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft> AddHandcraft(Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft> UpdateInfoCompany(Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft> UpdateRating(Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public Task<Handcraft> UpdateSubscription(Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public Task TakeOrder(UserAuthentication handcraft, int orderId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PushMailToCustomer(Feedback feedback)
    {
        throw new NotImplementedException();
    }

    public Task TakeWork(Work work, Company company, Handcraft handcraft)
    {
        throw new NotImplementedException();
    }

    public Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft)
    {
        throw new NotImplementedException();
    }
}