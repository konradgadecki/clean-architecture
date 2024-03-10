using ErrorOr;
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using MediatR;

public class CreateSubscriptionCommandHandler 
: IRequestHandler<CreateSubscriptionCommand, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        return Guid.NewGuid();
    }
}
