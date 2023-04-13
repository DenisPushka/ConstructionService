using Domain.Models;

namespace DataAccess.Interface;

public interface ISubscriptionRepository
{
    Task<Subscription[]> GetSubscriptions();
}