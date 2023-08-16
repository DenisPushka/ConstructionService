using Domain.Models;

namespace DataAccess.Interface;

/// <summary>
/// Интерфейс репозитория подписки.
/// </summary>
public interface ISubscriptionRepository
{
    /// <summary>
    /// Получение подписок.
    /// </summary>
    /// <returns>Массив подписок.</returns>
    Task<Subscription[]> GetSubscriptions();
}