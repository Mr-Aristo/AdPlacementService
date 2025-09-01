using AdPlacement.Domain.Abstractions;
using AdPlacement.Infrastructure.Index.Abstactions;
using AdPlacementDomain.Models;
using AdPlacementDomain.Models.Dtos;
using Microsoft.Extensions.Logging;

namespace AdPlacement.Infrastructure.Index;

public sealed class InMemoryPlacementsLoader : IPlacementLoader
{
    private readonly IPlacementIndexAccessor _accessor;
    private readonly ILogger<InMemoryPlacementsLoader> _logger;


    public InMemoryPlacementsLoader(IPlacementIndexAccessor accessor, ILogger<InMemoryPlacementsLoader> logger)
    {
        _accessor = accessor;
        _logger = logger;
    }


    public Task<LoadResult> ReloadAsync(string content, CancellationToken ct = default)
    {
        var builder = new IndexBuilder();


        foreach (var (_, segments) in FileParser.ParseLines(content, builder))
        {
            var pid = builder.PlatformNames.Count - 1; // last added id may not map 1:1 here, so re-fetch by name is safer
        }


        // We must parse again to add using fresh pid map (or add directly during parse)
        builder = new IndexBuilder();
        foreach (var item in FileParser.ParseLines(content, builder))
        {
            var pid = builder.GetOrAddPlatformId(item.platform);
            builder.AddOwn(pid, item.segments);
        }


        var index = builder.BuildEffective();
        _accessor.Swap(index);


        _logger.LogInformation("Placements reloaded: platforms={PlatformCount}, nodes={NodeCount}, skipped={Skipped}",
        index.PlatformCount, index.NodeCount, index.SkippedLineCount);


        var result = new LoadResult(true, index.PlatformCount, index.NodeCount, index.SkippedLineCount,
        Array.Empty<string>(), index.LoadedAt);
        return Task.FromResult(result);
    }
}