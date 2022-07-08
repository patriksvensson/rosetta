namespace Rosetta;

[DebuggerDisplay("{Id,nq} {Version,nq}")]
public sealed class TargetLibraryDependency
{
    public string Id { get; }
    public NuGetVersion Version { get; }

    public TargetLibraryDependency(string id, NuGetVersion version)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Version = version ?? throw new ArgumentNullException(nameof(version));
    }
}
