using AdPlacements.Domain.Abstractions;
using AdPlacements.Domain.Model;
using MediatR;


namespace AdPlacements.Application.Queries;


public sealed record GetPlatformsByLocationQuery(string Location) : IRequest<PlatformsResult>;


public sealed class GetPlatformsByLocationHandler : IRequestHandler<GetPlatformsByLocationQuery, PlatformsResult>
{
    private readonly IPlacementsQuery _query;
    public GetPlatformsByLocationHandler(IPlacementsQuery query) => _query = query;


    public Task<PlatformsResult> Handle(GetPlatformsByLocationQuery request, CancellationToken cancellationToken)
        => _query.GetPlatformsAsync(request.Location, cancellationToken);
}