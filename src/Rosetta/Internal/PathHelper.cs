namespace Rosetta.Internal;

internal static class PathHelper
{
    public static FilePath? GetFilePath(string? path)
    {
        return path == null ? null : new FilePath(path);
    }

    public static DirectoryPath? GetDirectoryPath(string? path)
    {
        return path == null ? null : new DirectoryPath(path);
    }
}
