namespace Rosetta;

[DebuggerDisplay("{Name,nq}")]
public sealed class Framework
{
    public string Name { get; }
    public string TargetAlias { get; }
    public ISet<FrameworkDependency> Dependencies { get; }
    public ISet<string> Imports { get; }
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
        Dependencies = new HashSet<FrameworkDependency>(dependencies);
        Imports = new HashSet<string>(imports);
        AssetTargetFallback = assetTargetFallback;
        RuntimeIdentifierGraphPath = runtimeIdentifierGraphPath;
    }
}
