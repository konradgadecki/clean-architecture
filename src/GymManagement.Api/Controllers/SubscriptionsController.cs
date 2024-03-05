using GymManagement.Application.Services;
using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpPost]
    public IActionResult CreateSubscription(CreateSubscription request)
    {
        var subscriptionId = _subscriptionService.CreateSubscription(
            request.SubscriptionType.ToString(), 
            request.AdminId);

        var response = new SubscriptionResponse(subscriptionId, request.SubscriptionType);

        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetSubscription()
    {
        return Ok();
    }
}