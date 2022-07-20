namespace Rosetta.Internal;

internal static class ExtensionMethods
{
    public static IReadOnlySet<T> ToReadOnlySet<T>(this IEnumerable<T>? source)
    {
        if (source == null)
        {
            return new HashSet<T>();
        }

        if (source is IReadOnlySet<T> set)
        {
            return set;
        }

        return new HashSet<T>(source);
    }

    public static IEnumerable<DirectoryPath> ToDirectoryPaths(this List<string>? source)
    {
        if (source == null)
        {
            return Enumerable.Empty<DirectoryPath>();
        }

        return source.Select(path => new DirectoryPath(path));
    }
}
