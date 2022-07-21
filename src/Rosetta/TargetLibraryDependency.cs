namespace Rosetta;

[DebuggerDisplay("{Id,nq} {Version,nq}")]
public sealed class TargetLibraryDependency
{
    public string Id { get; }
    public VersionRange Version { get; }

    public TargetLibraryDependency(string id, VersionRange version)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Version = version ?? throw new ArgumentNullException(nameof(version));
    }
}
