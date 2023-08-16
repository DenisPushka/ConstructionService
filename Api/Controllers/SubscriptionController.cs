using DataAccess.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

/// <summary>
/// Контроллер подписки.
/// </summary>
[ApiController, Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionRepository _subscription;

    public SubscriptionController(ISubscriptionRepository subscription)
    {
        _subscription = subscription;
    }

    /// <summary>
    /// Получение подписок.
    /// </summary>
    /// <returns>Массив подписок.</returns>
    [HttpGet("getSubscriptions")]
    public async Task<Subscription[]> GetSubscriptions()
    {
        return await _subscription.GetSubscriptions();
    }
}