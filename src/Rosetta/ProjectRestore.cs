namespace Rosetta;

[DebuggerDisplay("{Name,nq}")]
public sealed class ProjectRestore
{
    public string Name { get; }

    public FilePath ProjectPath { get; }

    public string UniqueName { get; }

    public DirectoryPath PackagesPath { get; }

    public DirectoryPath OutputPath { get; }

    public IReadOnlySet<DirectoryPath> FallbackFolders { get; }

    public ProjectStyle ProjectStyle { get; }

    public IReadOnlySet<ProjectFramework> ProjectFrameworks { get; }

    public ProjectRestore(
        string name, FilePath projectPath, string uniqueName,
        DirectoryPath packagesPath, IEnumerable<DirectoryPath> fallbackFolders,
        DirectoryPath outputPath, ProjectStyle projectStyle,
        IEnumerable<ProjectFramework> projectFrameworks)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        ProjectPath = projectPath ?? throw new ArgumentNullException(nameof(projectPath));
        UniqueName = uniqueName ?? throw new ArgumentNullException(nameof(uniqueName));
        PackagesPath = packagesPath ?? throw new ArgumentNullException(nameof(packagesPath));
        FallbackFolders = fallbackFolders.ToReadOnlySet();
        OutputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));
        ProjectStyle = projectStyle;
        ProjectFrameworks = projectFrameworks.ToReadOnlySet();
    }
}
