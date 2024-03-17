using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Domain.Subscriptions;
using MediatR;

public class CreateSubscriptionCommandHandler 
: IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private ISubscriptionsRepository _subscriptionsRepository;

    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = new Subscription()
        {
            Id = Guid.NewGuid()
        };

         _subscriptionsRepository.AddSubscription(subscription);

         return subscription;
    }
}
