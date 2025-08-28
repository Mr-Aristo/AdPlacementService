using AdReplacement.Domain.Models.Dtos;

namespace AdPlacement.Domain.Abstractions;

public interface IPlacementQuery
{
    Task<PlatformsResult> GetPlatformsAsync(string location, CancellationToken ct = default);
}
