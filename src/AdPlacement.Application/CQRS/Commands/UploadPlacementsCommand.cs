using AdPlacements.Domain.Abstractions;
using AdPlacements.Domain.Model;
using MediatR;


namespace AdPlacements.Application.Commands;


public sealed record UploadPlacementsCommand(string Content) : IRequest<LoadResult>;


public sealed class UploadPlacementsHandler : IRequestHandler<UploadPlacementsCommand, LoadResult>
{
    private readonly IPlacementsLoader _loader;
    public UploadPlacementsHandler(IPlacementsLoader loader) => _loader = loader;


    public Task<LoadResult> Handle(UploadPlacementsCommand request, CancellationToken cancellationToken)
        => _loader.ReloadAsync(request.Content, cancellationToken);
}      