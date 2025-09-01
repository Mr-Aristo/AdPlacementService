using AdPlacementDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdPlacement.Infrastructure.Index;

public static class FileParser
{
    public static IEnumerable<(string platform, string[] segments)> ParseLines(string content, IndexBuilder builder)
    {
        using var reader = new StringReader(content);
        string? line;
        int lineNo = 0;
        while ((line = reader.ReadLine()) is not null)
        {
            lineNo++;
            if (string.IsNullOrWhiteSpace(line)) continue;
            var raw = line.Trim();
            if (raw.StartsWith('#')) continue; // allow comments


            var idx = raw.IndexOf(':');
            if (idx <= 0 || idx == raw.Length - 1)
            {
                builder.IncrementSkipped();
                continue;
            }


            var name = raw[..idx].Trim();
            var right = raw[(idx + 1)..].Trim();
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(right))
            {
                builder.IncrementSkipped();
                continue;
            }


            var pid = builder.GetOrAddPlatformId(name);
            var tokens = right.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (tokens.Length == 0)
            {
                builder.IncrementSkipped();
                continue;
            }


            foreach (var t in tokens)
            {
                var norm = LocationPath.Normalize(t);
                var segs = LocationPath.SplitSegments(norm);
                yield return (name, segs);
            }
        }
    }
}
