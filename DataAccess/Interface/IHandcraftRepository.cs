using DataAccess.models;
using Domain.Models;
using Domain.Models.Service;
using Domain.Models.Users;

namespace DataAccess.Interface;

/// <summary>
/// Интерфейс для ремесленника (имеется схожесть с интерфейсом компании).
/// </summary>
public interface IHandcraftRepository
{
    Task<Handcraft> GetHandcraft(UserAuthentication handcraft);
    Task<Handcraft[]> GetAllHandcrafts();
    Task<Handcraft[]> GetHandcraftFromCity(string city);
    Task<Feedback[]> GetFeedbacks(Handcraft handcraft);
    Task<Work[]> GetWorks(UserAuthentication handcraft);
    Task<Equipment[]> GetEquipments(UserAuthentication handcraft);

    Task<Handcraft> AddHandcraft(Handcraft handcraft);
    Task<Handcraft> UpdateInfoHandcraft(Handcraft handcraft); 
    Task<Handcraft> UpdateRating(Handcraft handcraft);
    Task<Handcraft> UpdateSubscription(Handcraft handcraft);

    Task TakeOrder(UserAuthentication handcraft, int orderId);
    Task CompletedOrder(UserAuthentication handcraft, int orderId);
    Task RemoveOrder(int orderId);
    Task<bool> PushMailToCustomer(Feedback feedback);

    Task TakeWork(Work work, Company company, Handcraft handcraft);
    Task TakeEquipment(Equipment equipment, Company company, Handcraft handcraft);
}