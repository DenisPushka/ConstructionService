using DataAccess.Interface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController, Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionRepository _subscription;

    public SubscriptionController(ISubscriptionRepository subscription)
    {
        _subscription = subscription;
    }

    [HttpGet("getSubscriptions")]
    public async Task<Subscription[]> GetSubscriptions()
    {
        return await _subscription.GetSubscriptions();
    }
}