using DataAccess.Interface;
using Domain.Models;

namespace DataAccess.Realization;

/// <summary>
/// Репозиторий для подписки.
/// </summary>
public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly DataSql _sql;

    public SubscriptionRepository(DataSql sql) => _sql = sql;

    /// <summary>
    /// Получение подписок.
    /// </summary>
    /// <returns>Подписки.</returns>
    public async Task<Subscription[]> GetSubscriptions() => await _sql.GetSubscriptions();
}