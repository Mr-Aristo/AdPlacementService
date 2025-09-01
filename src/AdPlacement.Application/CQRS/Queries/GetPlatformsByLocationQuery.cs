using AdPlacement.Domain.Abstractions;
using AdPlacementDomain.Models.Dtos;
using MediatR;


namespace AdPlacements.Application.Queries;


public sealed record GetPlatformsByLocationQuery(string Location) : IRequest<PlatformsResult>;


public sealed class GetPlatformsByLocationHandler : IRequestHandler<GetPlatformsByLocationQuery, PlatformsResult>
{
    private readonly IPlacementQuery _query;
    public GetPlatformsByLocationHandler(IPlacementQuery query) => _query = query;


    public Task<PlatformsResult> Handle(GetPlatformsByLocationQuery request, CancellationToken cancellationToken)
        => _query.GetPlatformsAsync(request.Location, cancellationToken);
}