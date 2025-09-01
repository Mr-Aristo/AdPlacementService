
using AdPlacementDomain.Models;
using FluentAssertions;

namespace AdPlacement.Tests;

public class IndexBuilderTests
{
    [Fact]
    public void BuildEffective_Should_Combine_Parent_And_Own()
    {
        var b = new IndexBuilder();
        var y = b.GetOrAddPlatformId("Yandex");
        var k = b.GetOrAddPlatformId("CoolAds");


        b.AddOwn(y, Array.Empty<string>()); // "/"
        b.AddOwn(k, new[] { "ru", "svrd" });


        var idx = b.BuildEffective();
        var root = idx.Root;
        root.EffectivePlatformIds.Should().HaveCount(1);


        var node = root.Children["ru"].Children["svrd"];
        idx.MapIdsToNames(node.EffectivePlatformIds).Should().Contain(new[] { "CoolAds", "Yandex" });
    }
}
