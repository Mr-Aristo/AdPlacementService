
using AdPlacement.Domain.Abstractions;
using AdPlacementDomain.Models.Dtos;
using MediatR;


namespace AdPlacements.Application.Commands;


public sealed record UploadPlacementsCommand(string Content) : IRequest<LoadResult>;


public sealed class UploadPlacementsHandler : IRequestHandler<UploadPlacementsCommand, LoadResult>
{
    private readonly IPlacementLoader _loader;
    public UploadPlacementsHandler(IPlacementLoader loader) => _loader = loader;


    public Task<LoadResult> Handle(UploadPlacementsCommand request, CancellationToken cancellationToken)
        => _loader.ReloadAsync(request.Content, cancellationToken);
}      