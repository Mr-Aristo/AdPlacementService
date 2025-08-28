using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdReplacement.Domain.Models;

public static class LocationPath
{
    public static string Normalize(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData)) return "/";

        var s = rawData.Trim();

        if (s.StartsWith("/")) s = "/" + s;

        while (s.Contains("//")) s = s.Replace("//", "/");

        if (s.Length > 1 && s.EndsWith('/')) s = s.TrimEnd('/');

        return s;
    }

    public static string[] SplitSegments(string normalized)
    {
        if(normalized == "/") return Array.Empty<string>();

        return normalized.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
    }
}
