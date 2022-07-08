using Rosetta.Internal;

namespace Rosetta;

public sealed class AssetFile
{
    public IReadOnlySet<Target> Targets { get; }
    public IReadOnlySet<Library> Libraries { get; }
    public Project Project { get; }

    public AssetFile(
        IEnumerable<Target> targets,
        IEnumerable<Library> libraries,
        Project project)
    {
        ArgumentNullException.ThrowIfNull(targets);
        ArgumentNullException.ThrowIfNull(libraries);
        ArgumentNullException.ThrowIfNull(project);

        if (targets is null)
        {
            throw new ArgumentNullException(nameof(targets));
        }

        Targets = new HashSet<Target>(targets);
        Libraries = new HashSet<Library>(libraries);
        Project = project;
    }

    public static AssetFile Parse(string json)
    {
        var obj = JsonConvert.DeserializeObject<JsonModel.Assets>(json);
        if (obj == null)
        {
            throw new InvalidOperationException("Could not parse project.assets.json.");
        }

        return JsonModelMapper.Map(obj);
    }
}
