using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetGym;

public class GetGymQueryHandler : IRequestHandler<GetGymQuery, ErrorOr<Gym>>
{
    private readonly IGymsRepository _gymsRepository;
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetGymQueryHandler(
        IGymsRepository gymsRepository,
        ISubscriptionsRepository subscriptionsRepository
    )
    {
        _gymsRepository = gymsRepository;
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Gym>> Handle(GetGymQuery request, CancellationToken cancellationToken)
    {
        var subExists = await _subscriptionsRepository.ExistsAsync(request.SubscriptionId);
        if (!subExists)
        {
            return Error.NotFound("Subscription not found");
        }

        var gym = await _gymsRepository.GetByIdAsync(request.GymId);
        if (gym is null)
        {
            return Error.NotFound(description: "Gym not found");
        }

        return gym;
    }
}
