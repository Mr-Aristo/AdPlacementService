using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdReplacement.Domain.Models.Dtos;

public sealed record LoadResult(
    bool Success,
    int PlatformCount,
    int NodeCount,
    int SkippedLineCount, 
    IReadOnlyList<string> SkippedSamples,
    DateTimeOffset LoadedAt
    );

