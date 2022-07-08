namespace Rosetta;

[DebuggerDisplay("{Name,nq}")]
public sealed class ProjectFramework
{
    public string Name { get; set; }
    public string TargetAlias { get; set; }
    public ISet<ProjectReference> ProjectReferences { get; }

    public ProjectFramework(string name, string targetAlias, IEnumerable<ProjectReference> projectReferences)
    {
        Name = name;
        TargetAlias = targetAlias;
        ProjectReferences = new HashSet<ProjectReference>(projectReferences ?? Enumerable.Empty<ProjectReference>());
    }
}
