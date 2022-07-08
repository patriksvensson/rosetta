namespace Rosetta;

[DebuggerDisplay("{Name,nq}")]
public sealed class ProjectRestore
{
    public string Name { get; }

    public string Path { get; }

    public string UniqueName { get; }

    public string PackagesPath { get; }

    public string OutputPath { get; }

    public ProjectStyle ProjectStyle { get; }

    public ISet<ProjectFramework> ProjectFrameworks { get; }

    public ProjectRestore(
        string name, string path, string uniqueName,
        string packagesPath, string outputPath,
        ProjectStyle projectStyle,
        IEnumerable<ProjectFramework> projectFrameworks)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Path = path ?? throw new ArgumentNullException(nameof(path));
        UniqueName = uniqueName ?? throw new ArgumentNullException(nameof(uniqueName));
        PackagesPath = packagesPath ?? throw new ArgumentNullException(nameof(packagesPath));
        OutputPath = outputPath ?? throw new ArgumentNullException(nameof(outputPath));
        ProjectStyle = projectStyle;
        ProjectFrameworks = new HashSet<ProjectFramework>(projectFrameworks ?? Enumerable.Empty<ProjectFramework>());
    }
}
