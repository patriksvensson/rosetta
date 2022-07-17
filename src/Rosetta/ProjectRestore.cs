using Spectre.IO;

namespace Rosetta;

[DebuggerDisplay("{Name,nq}")]
public sealed class ProjectRestore
{
    public string Name { get; }

    public FilePath ProjectPath { get; }

    public string UniqueName { get; }

    public DirectoryPath PackagesPath { get; }

    public DirectoryPath OutputPath { get; }

    public ProjectStyle ProjectStyle { get; }

    public ISet<ProjectFramework> ProjectFrameworks { get; }

    public ProjectRestore(
        string name, FilePath projectPath, string uniqueName,
        DirectoryPath packagesPath, DirectoryPath outputPath,
        ProjectStyle projectStyle,
        IEnumerable<ProjectFramework> projectFrameworks)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        ProjectPath = projectPath ?? throw new ArgumentNullException(nameof(projectPath));
        UniqueName = uniqueName ?? throw new ArgumentNullException(nameof(uniqueName));
        PackagesPath = packagesPath ?? throw new ArgumentNullException(nameof(packagesPath));
        OutputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));
        ProjectStyle = projectStyle;
        ProjectFrameworks = new HashSet<ProjectFramework>(projectFrameworks ?? Enumerable.Empty<ProjectFramework>());
    }
}
