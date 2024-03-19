namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
    private readonly Guid _adminId;
    public Guid Id { get; private set; }

    public SubscriptionType SubscriptionType { get; private set; }

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null)
    {
        SubscriptionType = subscriptionType;
        _adminId = adminId;
        Id = id ?? Guid.NewGuid();
    }

    // needed to allow entity framework to create via reflection 
    // an instance of the subscription 
    private Subscription(){}
}