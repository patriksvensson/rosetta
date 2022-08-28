namespace Rosetta;

[DebuggerDisplay("{Name,nq} {VersionRange.OriginalString,nq}")]
public sealed class DownloadDependency
{
    public string Name { get; }
    public VersionRange VersionRange { get; }

    public DownloadDependency(
        string name,
        VersionRange versionRange)
    {
        Name = name;
        VersionRange = versionRange;
    }
}
