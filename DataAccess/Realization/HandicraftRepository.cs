using System;
using System.Threading.Tasks;
using DataAccess.Interface;
using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Realization;

public class HandcraftRepository : IHandcraftRepository
{
    private readonly DataSqlHandCraft _sqlHandCraft;
    private readonly DataSqlService _sqlService;
    private readonly DataSqlFeedBack _sqlFeedBack;

    public HandcraftRepository(DataSqlHandCraft sqlHandCraft, DataSqlService sqlService, DataSqlFeedBack sqlFeedBack)
    {
        _sqlHandCraft = sqlHandCraft;
        _sqlService = sqlService;
        _sqlFeedBack = sqlFeedBack;
    }

    public async Task<Handcraft> GetHandcraft(UserAuthentication handcraft) => await _sqlHandCraft.Get(handcraft);

    public async Task<Handcraft[]> GetAllHandcrafts() => await _sqlHandCraft.GetHandcrafts();

    public async Task<Handcraft[]> GetHandcraftFromCity(string city) => await _sqlHandCraft.GetHandCraftsFromCity(city);

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

    public async Task<Handcraft> AddHandcraft(Handcraft handcraft)
    {
        return await _sqlHandCraft.Add(handcraft);
    }

    public async Task<Handcraft> UpdateInfoHandcraft(Handcraft handcraft)
    {
        return await _sqlHandCraft.UpdateInfoHandCraft(handcraft);
    }

    public async Task<Handcraft> UpdateRating(Handcraft handcraft)
    {
        return await _sqlHandCraft.UpdateRating(handcraft);
    }

    public async Task<Handcraft> UpdateSubscription(Handcraft handcraft)
    {
        return await _sqlHandCraft.UpdateSubscription(handcraft);
    }

    public async Task TakeOrder(UserAuthentication handcraft, int orderId)
    {
        await _sqlHandCraft.TakeOrder(handcraft, orderId);
    }

    public async Task CompletedOrder(UserAuthentication handcraft, int orderId)
    {
        await _sqlHandCraft.CompletedOrder(handcraft, orderId);
    }

    public async Task RemoveOrder(int orderId)
    {
        await _sqlHandCraft.RemoveOrder(orderId);
    }

    public async Task<bool> PushMailToCustomer(Feedback feedback)
    {
        return await _sqlFeedBack.SendToContactor(feedback);
    }

    public async Task TakeWork(Work work, Company company, Handcraft handcraft)
    {
        await _sqlService.TakeWork(work, company, handcraft);
    }

    public async Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft)
    {
        await _sqlService.TakeEquipment(equipment, company, handcraft);
    }
}