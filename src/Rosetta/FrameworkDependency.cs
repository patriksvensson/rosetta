namespace Rosetta;

[DebuggerDisplay("{Name,nq} {VersionRange.OriginalString,nq}")]
public sealed class FrameworkDependency
{
    public string Name { get; }
    public FrameworkDependencyTarget Target { get; }
    public VersionRange VersionRange { get; }

    public FrameworkDependency(
        string name,
        FrameworkDependencyTarget target,
        VersionRange versionRange)
    {
        Name = name;
        Target = target;
        VersionRange = versionRange;
    }
}
