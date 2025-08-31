using AdPlacement.Infrastructure.Index;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace AdPlacement.Tests;

public class QueryTests
{
    [Fact]
    public async Task Query_Returns_Deepest_Match()
    {
        var accessor = new PlacementIndexAccessor();
        var loader = new InMemoryPlacementsLoader(accessor, new LoggerFactory().CreateLogger<InMemoryPlacementsLoader>());


        var text = "Yandex:/ru\nLocal:/ru/svrd/revda\nCool:/ru/svrd\n";
        await loader.ReloadAsync(text);


        var query = new InMemoryPlacementQuery(accessor);
        var result = await query.GetPlatformsAsync("/ru/svrd/revda");


        result.Platforms.Should().Contain(new[] { "Cool", "Local", "Yandex" });
        result.MatchedDepth.Should().Be(3);
    }
}
