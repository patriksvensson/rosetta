namespace Rosetta;

[DebuggerDisplay("{UniqueName,nq}")]
public sealed class ProjectReference
{
    public string UniqueName { get; }
    public FilePath ProjectPath { get; }
    public IncludeFlags IncludeAssets { get; }
    public IncludeFlags ExcludeAssets { get; }
    public IncludeFlags PrivateAssets { get; }

    public ProjectReference(
        string uniqueName, FilePath projectPath,
        IncludeFlags includeAssets,
        IncludeFlags excludeAssets,
        IncludeFlags privateAssets)
    {
        UniqueName = uniqueName;
        ProjectPath = projectPath;
        IncludeAssets = includeAssets;
        ExcludeAssets = excludeAssets;
        PrivateAssets = privateAssets;
    }
}
