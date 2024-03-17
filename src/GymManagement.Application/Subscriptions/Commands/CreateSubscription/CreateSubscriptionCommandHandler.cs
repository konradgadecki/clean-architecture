using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Domain.Subscriptions;
using MediatR;

public class CreateSubscriptionCommandHandler 
: IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
   // private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(
        // IUnitOfWork unitOfWork,
        ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
        //_unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscription = new Subscription()
        {
            Id = Guid.NewGuid(),
            SubscriptionType = request.SubscriptionType
        };

        await _subscriptionsRepository.AddSubscriptionAsync(subscription);
        //await _unitOfWork.CommitChangesAsync();

         return subscription;
    }
}
