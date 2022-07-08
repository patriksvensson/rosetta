using System.Diagnostics.CodeAnalysis;

namespace Rosetta.Internal;

internal static class StringSplitter
{
    public static bool SplitTwo(
        string text,
        [NotNullWhen(true)] out string? first,
        [NotNullWhen(true)] out string? second,
        char separator = '/')
    {
        var parts = text.Split(new[] { separator });
        if (parts.Length != 2)
        {
            first = null;
            second = null;
            return false;
        }

        first = parts[0];
        second = parts[1];
        return true;
    }

    public static bool SplitMaxTwo(
        string text,
        [NotNullWhen(true)] out string? first,
        [MaybeNullWhen(true)] out string? second,
        char separator = '/')
    {
        var parts = text.Split(new[] { separator });
        if (parts.Length == 1)
        {
            first = parts[0];
            second = null;
            return true;
        }

        if (parts.Length == 2)
        {
            first = parts[0];
            second = parts[1];
            return true;
        }

        first = null;
        second = null;
        return false;
    }
}
