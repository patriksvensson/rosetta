namespace Rosetta;

public sealed class Project
{
    public NuGetVersion Version { get; }
    public ProjectRestore Restore { get; }
    public IReadOnlySet<Framework> Frameworks { get; }

    public Project(NuGetVersion version, ProjectRestore restore, IEnumerable<Framework> frameworks)
    {
        Version = version ?? throw new ArgumentNullException(nameof(version));
        Restore = restore ?? throw new ArgumentNullException(nameof(restore));
        Frameworks = frameworks.ToReadOnlySet();
    }
}
