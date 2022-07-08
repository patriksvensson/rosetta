using System;
using System.Diagnostics;
using System.IO;

namespace Rosetta.Tests;

public static class EmbeddedResourceReader
{
    public static string Read(string resourceName)
    {
        if (resourceName is null)
        {
            throw new ArgumentNullException(nameof(resourceName));
        }

        var frame = new StackFrame(1);
        var assembly = frame.GetMethod()?.DeclaringType?.Assembly;
        if (assembly == null)
        {
            throw new InvalidOperationException("Could not resolve caller.");
        }

        resourceName = resourceName.Replace("/", ".", StringComparison.Ordinal);
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new InvalidOperationException("Could not load manifest resource stream.");
            }

            using (var reader = new StreamReader(stream))
            {
                return NormalizeLineEndings(reader.ReadToEnd());
            }
        }
    }

    public static Stream GetStream(string resourceName)
    {
        if (resourceName is null)
        {
            throw new ArgumentNullException(nameof(resourceName));
        }

        var frame = new StackFrame(1);
        var assembly = frame.GetMethod()?.DeclaringType?.Assembly;
        if (assembly == null)
        {
            throw new InvalidOperationException("Could not resolve caller.");
        }

        resourceName = resourceName.Replace("/", ".", StringComparison.Ordinal);
        return assembly.GetManifestResourceStream(resourceName);
    }

    private static string NormalizeLineEndings(string value)
    {
        if (value != null)
        {
            value = value.Replace("\r\n", "\n", StringComparison.Ordinal);
            value = value.Replace("\r", string.Empty, StringComparison.Ordinal);
            return value.Replace("\n", "\r\n", StringComparison.Ordinal);
        }

        return string.Empty;
    }
}