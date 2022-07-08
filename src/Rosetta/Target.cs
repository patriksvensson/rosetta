namespace Rosetta;

[DebuggerDisplay("{DebugString(),nq}")]
public sealed class Target
{
    public string TargetFramework { get; }
    public string? RuntimeIdentifier { get; }

    public IReadOnlySet<TargetLibrary> Libraries { get; }

    public Target(string targetFramework, string? runtimeIdentifier, IEnumerable<TargetLibrary> libraries)
    {
        TargetFramework = targetFramework ?? throw new ArgumentNullException(nameof(targetFramework));
        RuntimeIdentifier = runtimeIdentifier;
        Libraries = new HashSet<TargetLibrary>(libraries);
    }

    private string DebugString()
    {
        if (RuntimeIdentifier == null)
        {
            return TargetFramework;
        }

        return TargetFramework + " (" + RuntimeIdentifier + ")";
    }
}
