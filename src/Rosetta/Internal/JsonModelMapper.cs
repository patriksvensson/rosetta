using Spectre.IO;

namespace Rosetta.Internal;

internal static class JsonModelMapper
{
    public static AssetFile Map(JsonModel.Assets assets)
    {
        return new AssetFile(
            ParseTargets(assets.Targets),
            ParseLibraries(assets.Libraries),
            ParseProject(assets.Project));
    }

    private static IEnumerable<Target> ParseTargets(Dictionary<string, Dictionary<string, JsonModel.TargetLibrary>>? targets)
    {
        if (targets == null)
        {
            return Enumerable.Empty<Target>();
        }

        var result = new List<Target>();
        foreach (var (targetName, targetLibraries) in targets)
        {
            if (!StringSplitter.SplitMaxTwo(targetName, out var tfm, out var rmi, separator: '/'))
            {
                throw new InvalidOperationException("Could not parse target name");
            }

            var libraries = new List<TargetLibrary>();
            foreach (var targetLibrary in targetLibraries)
            {
                libraries.Add(ParseTargetLibrary(targetLibrary.Key, targetLibrary.Value));
            }

            result.Add(new Target(tfm, rmi, libraries));
        }

        return result;
    }

    private static TargetLibrary ParseTargetLibrary(string libraryName, JsonModel.TargetLibrary library)
    {
        if (!StringSplitter.SplitTwo(libraryName, out var name, out var version, '/'))
        {
            throw new InvalidOperationException("Encountered invalid target library name");
        }

        var type = ParseLibraryType(library.Type);

        var dependencies = new List<TargetLibraryDependency>();
        if (library.Dependencies != null)
        {
            foreach (var (dependencyName, dependencyVersion) in library.Dependencies)
            {
                dependencies.Add(new TargetLibraryDependency(
                    dependencyName,
                    new NuGetVersion(
                        dependencyVersion?.Trim('[', ']') ?? "0.0.0.0")));
            }
        }

        return new TargetLibrary(
            name, new NuGetVersion(version ?? "0.0.0.0"),
            type, dependencies);
    }

    private static IEnumerable<Library> ParseLibraries(Dictionary<string, JsonModel.Library>? libraries)
    {
        if (libraries == null)
        {
            return Enumerable.Empty<Library>();
        }

        var result = new List<Library>();
        foreach (var (libraryName, library) in libraries)
        {
            if (!StringSplitter.SplitTwo(libraryName, out var name, out var version, '/'))
            {
                throw new InvalidOperationException("Encountered invalid library name");
            }

            var files = new List<string>();
            if (library.Files != null)
            {
                foreach (var file in library.Files)
                {
                    files.Add(file);
                }
            }

            result.Add(new Library(
                name, new NuGetVersion(version ?? "0.0.0.0"), ParseLibraryType(library.Type),
                library.Sha512,
                PathHelper.GetDirectoryPath(library.Path),
                library.HasTools,
                files));
        }

        return result;
    }

    private static Project ParseProject(JsonModel.Project? project)
    {
        if (project == null)
        {
            throw new InvalidOperationException("Assets file is missing project section");
        }

        if (project.Restore == null)
        {
            throw new InvalidOperationException("Assets file is missing project restore section");
        }

        return new Project(
            version: new NuGetVersion(project.Version ?? "0.0.0.0"),
            restore: new ProjectRestore(
                name: project.Restore.ProjectName!,
                projectPath: new FilePath(project.Restore.ProjectPath!),
                uniqueName: project.Restore.ProjectUniqueName!,
                packagesPath: new DirectoryPath(project.Restore.PackagesPath!),
                outputPath: new DirectoryPath(project.Restore.OutputPath!),
                projectStyle: ParseProjectStyle(project.Restore.ProjectStyle),
                projectFrameworks: ParseProjectFrameworks(project.Restore.Frameworks)),
            frameworks: ParseFrameworks(project.Frameworks));
    }

    private static IEnumerable<Framework> ParseFrameworks(Dictionary<string, JsonModel.Project.Framework>? frameworks)
    {
        var result = new List<Framework>();

        if (frameworks != null)
        {
            foreach (var (frameworkName, framework) in frameworks)
            {
                result.Add(new Framework(
                        name: frameworkName,
                        targetAlias: framework.TargetAlias!,
                        dependencies: ParseFrameworkDependencies(framework.Dependencies),
                        imports: framework.Imports ?? Enumerable.Empty<string>(),
                        assetTargetFallback: framework.AssetTargetFallback,
                        runtimeIdentifierGraphPath: framework.RuntimeIdentifierGraphPath ?? string.Empty));
            }
        }

        return result;
    }

    private static IEnumerable<FrameworkDependency> ParseFrameworkDependencies(Dictionary<string, JsonModel.Project.Framework.Dependency>? dependencies)
    {
        var result = new List<FrameworkDependency>();
        if (dependencies != null)
        {
            foreach (var (dependencyName, dependency) in dependencies)
            {
                result.Add(new FrameworkDependency(
                    dependencyName,
                    Enum.Parse<FrameworkDependencyTarget>(dependency.Target ?? "None"),
                    VersionRange.Parse(dependency.Version)));
            }
        }

        return result;
    }

    private static IEnumerable<ProjectFramework> ParseProjectFrameworks(
        Dictionary<string, JsonModel.Project.ProjectRestore.FrameworkGroup>? frameworks)
    {
        var result = new List<ProjectFramework>();

        if (frameworks != null)
        {
            foreach (var framework in frameworks)
            {
                result.Add(new ProjectFramework(
                    framework.Key,
                    framework.Value.TargetAlias ?? framework.Key,
                    ParseProjectReferences(framework.Value.ProjectReferences)));
            }
        }

        return result;
    }

    private static IEnumerable<ProjectReference> ParseProjectReferences(
        Dictionary<string, JsonModel.Project.ProjectRestore.FrameworkGroup.ProjectReference>? projectReferences)
    {
        var result = new List<ProjectReference>();

        if (projectReferences != null)
        {
            foreach (var projectReference in projectReferences)
            {
                result.Add(new ProjectReference(
                    projectReference.Key,
                    projectPath: new FilePath(projectReference.Value.ProjectPath ?? projectReference.Key),
                    includeAssets: IncludeFlagsParser.GetFlags(projectReference.Value.IncludeAssets, IncludeFlags.All),
                    excludeAssets: IncludeFlagsParser.GetFlags(projectReference.Value.ExcludeAssets, IncludeFlags.None),
                    privateAssets: IncludeFlagsParser.GetFlags(projectReference.Value.ExcludeAssets, IncludeFlags.Build | IncludeFlags.ContentFiles | IncludeFlags.Analyzers)));
            }
        }

        return result;
    }

    private static LibraryType ParseLibraryType(string? text)
    {
        return text?.ToLowerInvariant() switch
        {
            "package" => LibraryType.Package,
            "project" => LibraryType.Project,
            _ => LibraryType.Unknown,
        };
    }

    private static ProjectStyle ParseProjectStyle(string? text)
    {
        if (text == null)
        {
            return ProjectStyle.Unknown;
        }

        return Enum.Parse<ProjectStyle>(text);
    }
}
