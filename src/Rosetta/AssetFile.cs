using System.IO;
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

        Targets = targets.ToReadOnlySet();
        Libraries = libraries.ToReadOnlySet();
        Project = project;
    }

    public static AssetFile FromStream(Stream stream)
    {
        return Parse(() =>
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        });
    }

    public static AssetFile FromFile(string path)
    {
        return Parse(() => File.ReadAllText(path));
    }

    public static AssetFile FromJson(string json)
    {
        return Parse(() => json);
    }

    private static AssetFile Parse(Func<string> func)
    {
        try
        {
            var json = func();

            var obj = JsonConvert.DeserializeObject<JsonModel.Assets>(json);
            if (obj == null)
            {
                throw new InvalidOperationException("Could not parse project.assets.json.");
            }

            return JsonModelMapper.Map(obj);
        }
        catch (Exception ex)
        {
            throw new RosettaException("Could not parse project.assets.json. See inner exception for more information", ex);
        }
    }
}
