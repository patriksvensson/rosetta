namespace Rosetta;

[DebuggerDisplay("{Name,nq}")]
public sealed class Framework
{
    public string Name { get; }
    public string TargetAlias { get; }
    public IReadOnlySet<FrameworkDependency> Dependencies { get; }
    public IReadOnlySet<string> Imports { get; }
    public bool AssetTargetFallback { get; }
    public string RuntimeIdentifierGraphPath { get; }

    public Framework(
        string name, string targetAlias,
        IEnumerable<FrameworkDependency> dependencies,
        IEnumerable<string> imports,
        bool assetTargetFallback,
        string runtimeIdentifierGraphPath)
    {
        Name = name;
        TargetAlias = targetAlias;
        Dependencies = dependencies.ToReadOnlySet();
        Imports = imports.ToReadOnlySet();
        AssetTargetFallback = assetTargetFallback;
        RuntimeIdentifierGraphPath = runtimeIdentifierGraphPath;
    }
}
