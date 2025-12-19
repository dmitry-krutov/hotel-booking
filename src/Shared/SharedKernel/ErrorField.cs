using System;
using System.Linq;

namespace SharedKernel;

public static class ErrorField
{
    public static string? Normalize(string? rawFieldPath)
    {
        if (string.IsNullOrWhiteSpace(rawFieldPath))
            return rawFieldPath;

        var segments = rawFieldPath.Split('.', StringSplitOptions.RemoveEmptyEntries);
        return string.Join('.', segments.Select(NormalizeSegment));
    }

    private static string NormalizeSegment(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment))
            return segment;

        return segment.Length == 1
            ? segment.ToLowerInvariant()
            : char.ToLowerInvariant(segment[0]) + segment[1..];
    }
}
