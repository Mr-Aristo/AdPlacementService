using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdPlacementDomain.Models
{
    public sealed class TrieNode
    {
        public Dictionary<string, TrieNode> Children { get; } = new(StringComparer.OrdinalIgnoreCase);
        public HashSet<int> OwnPlatforms { get; } = new HashSet<int>();
        public int[] EffectivePlatformIds { get; set; } = Array.Empty<int>();
    }
}
