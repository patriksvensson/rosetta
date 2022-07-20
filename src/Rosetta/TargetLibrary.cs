namespace Rosetta;

[DebuggerDisplay("{Name,nq} {Version,nq}")]
public sealed class TargetLibrary
{
    public string Name { get; }
    public NuGetVersion Version { get; }
    public LibraryType Type { get; }
    public IReadOnlySet<TargetLibraryDependency> Dependencies { get; }

    public TargetLibrary(string name, NuGetVersion version, LibraryType type, IEnumerable<TargetLibraryDependency> dependencies)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Version = version ?? throw new ArgumentNullException(nameof(version));
        Type = type;
        Dependencies = dependencies.ToReadOnlySet();
    }
}
