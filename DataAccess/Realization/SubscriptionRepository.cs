using DataAccess.Interface;
using Domain.Models;

namespace DataAccess.Realization;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly DataSql _sql;

    public SubscriptionRepository(DataSql sql)
    {
        _sql = sql;
    }

    public async Task<Subscription[]> GetSubscriptions()
    {
        return await _sql.GetSubscriptions();
    }
}