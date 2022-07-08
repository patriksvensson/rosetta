// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Rosetta.Internal;

internal static class IncludeFlagsParser
{
    // ref: https://github.com/NuGet/NuGet.Client/blob/dev/src/NuGet.Core/NuGet.LibraryModel/LibraryDependencyTargetUtils.cs
    public static IncludeFlags GetFlags(string? flags, IncludeFlags defaultFlags)
    {
        if (string.IsNullOrWhiteSpace(flags))
        {
            return defaultFlags;
        }

        var result = defaultFlags;

        var splitFlags = flags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrEmpty(s))
            .ToArray();

        if (splitFlags.Length > 0)
        {
            result = GetFlags(splitFlags);
        }

        return result;
    }

    // ref: https://github.com/NuGet/NuGet.Client/blob/dev/src/NuGet.Core/NuGet.LibraryModel/LibraryDependencyTargetUtils.cs
    public static IncludeFlags GetFlags(IEnumerable<string> flags)
    {
        if (flags == null)
        {
            throw new ArgumentNullException(nameof(flags));
        }

        var result = IncludeFlags.None;

        foreach (var flag in flags)
        {
            switch (flag.ToLowerInvariant())
            {
                case "all":
                    result |= IncludeFlags.All;
                    break;
                case "runtime":
                    result |= IncludeFlags.Runtime;
                    break;
                case "compile":
                    result |= IncludeFlags.Compile;
                    break;
                case "build":
                    result |= IncludeFlags.Build;
                    break;
                case "contentfiles":
                    result |= IncludeFlags.ContentFiles;
                    break;
                case "native":
                    result |= IncludeFlags.Native;
                    break;
                case "analyzers":
                    result |= IncludeFlags.Analyzers;
                    break;
                case "buildtransitive":
                    result |= IncludeFlags.BuildTransitive | IncludeFlags.Build;
                    break;

                    // None is a noop here
            }
        }

        return result;
    }
}