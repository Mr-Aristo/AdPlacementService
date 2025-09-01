using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdPlacementDomain.Models;

public sealed class IndexBuilder
{
    private readonly TrieNode _root = new();
    private readonly Dictionary<string, int> _nameToId = new(StringComparer.Ordinal);
    private readonly List<string> _idToName = new();


    private int _nodeCount = 1; // root
    private int _skipped;


    public IReadOnlyList<string> PlatformNames => _idToName;
    public int NodeCount => _nodeCount;
    public int SkippedLineCount => _skipped;


    public int GetOrAddPlatformId(string name)
    {
        if (_nameToId.TryGetValue(name, out var id)) return id;
        id = _idToName.Count;
        _nameToId[name] = id;
        _idToName.Add(name);
        return id;
    }


    public void AddOwn(int platformId, string[] segments)
    {
        var node = _root;
        foreach (var seg in segments)
        {
            if (!node.Children.TryGetValue(seg, out var next))
            {
                next = new TrieNode();
                node.Children[seg] = next;
                _nodeCount++;
            }
            node = next;
        }
        node.OwnPlatforms.Add(platformId);
    }


    public void IncrementSkipped() => _skipped++;


    public PlatformIndex BuildEffective()
    {
        var names = _idToName.ToArray();
        var comparer = Comparer<int>.Create((a, b) => string.Compare(names[a], names[b], StringComparison.Ordinal));


        void Dfs(TrieNode node, HashSet<int> inherited)
        {
            var set = new HashSet<int>(inherited);
            set.UnionWith(node.OwnPlatforms);
            var arr = set.ToArray();
            Array.Sort(arr, comparer);
            node.EffectivePlatformIds = arr;


            foreach (var child in node.Children.Values)
                Dfs(child, set);
        }


        Dfs(_root, new HashSet<int>());
        return new PlatformIndex(_root, names, isLoaded: true, _nodeCount, _idToName.Count, _skipped, DateTimeOffset.UtcNow);
    }
}