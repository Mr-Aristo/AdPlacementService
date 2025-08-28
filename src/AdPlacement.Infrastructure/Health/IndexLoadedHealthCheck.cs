
using AdPlacement.Infrastructure.Index.Abstactions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AdPlacement.Infrastructure.Health;

public sealed class IndexLoadedHealthCheck : IHealthCheck
{
    private readonly IPlacementIndexAccessor _accessor;
    public IndexLoadedHealthCheck(IPlacementIndexAccessor accessor) => _accessor = accessor;
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var cur = _accessor.Current;
        var data = new Dictionary<string, object?>
        {
            ["isLoaded"] = cur.IsLoaded,
            ["loadedAt"] = cur.LoadedAt,
            ["platformCount"] = cur.PlatformCount,
            ["nodeCount"] = cur.NodeCount,
            ["skippedLineCount"] = cur.SkippedLineCount
        };


        return Task.FromResult(cur.IsLoaded
        ? HealthCheckResult.Healthy("Index loaded", data: data)
        : HealthCheckResult.Degraded("Index not loaded", data: data));
    }
}
