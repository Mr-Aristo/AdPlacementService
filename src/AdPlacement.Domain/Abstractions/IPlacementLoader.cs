using AdReplacement.Domain.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdPlacement.Domain.Abstractions;

public interface IPlacementLoader
{
    Task<LoadResult> ReloadAsync(string content, CancellationToken ct = default);
}
