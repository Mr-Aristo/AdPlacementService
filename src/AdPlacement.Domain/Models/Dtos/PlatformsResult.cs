using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdPlacementDomain.Models.Dtos;

public sealed record PlatformsResult(
    string Location,
    IReadOnlyList<string> Platforms,
    int MatchedDepth

    );

