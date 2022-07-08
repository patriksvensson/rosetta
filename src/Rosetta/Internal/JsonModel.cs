namespace Rosetta.Internal;

internal sealed class JsonModel
{
    public static Assets? Parse(string json)
    {
        return JsonConvert.DeserializeObject<Assets>(json);
    }

    public sealed class Assets
    {
        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("libraries")]
        public Dictionary<string, Library>? Libraries { get; set; }

        [JsonProperty("project")]
        public Project? Project { get; set; }

        [JsonProperty("targets")]
        public Dictionary<string, Dictionary<string, TargetLibrary>>? Targets { get; set; }
    }

    public sealed class TargetLibrary
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("dependencies")]
        public Dictionary<string, string>? Dependencies { get; set; }
    }

    public sealed class Library
    {
        [JsonProperty("sha512")]
        public string? Sha512 { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }

        [JsonProperty("hasTools")]
        public bool? HasTools { get; set; }

        [JsonProperty("files")]
        public List<string>? Files { get; set; }
    }

    public sealed class Project
    {
        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("frameworks")]
        public Dictionary<string, Framework>? Frameworks { get; set; }

        [JsonProperty("restore")]
        public ProjectRestore? Restore { get; set; }

        public sealed class Framework
        {
            [JsonProperty("targetAlias")]
            public string? TargetAlias { get; set; }

            [JsonProperty("dependencies")]
            public Dictionary<string, Dependency>? Dependencies { get; set; }

            [JsonProperty("imports")]
            public List<string>? Imports { get; set; }

            [JsonProperty("assetTargetFallback")]
            public bool AssetTargetFallback { get; set; }

            [JsonProperty("runtimeIdentifierGraphPath")]
            public string? RuntimeIdentifierGraphPath { get; set; }

            public sealed class Dependency
            {
                [JsonProperty("include")]
                public string? Include { get; set; }

                [JsonProperty("suppressParent")]
                public string? SuppressParent { get; set; }

                [JsonProperty("target")]
                public string? Target { get; set; }

                [JsonProperty("version")]
                public string? Version { get; set; }
            }
        }

        public sealed class ProjectRestore
        {
            [JsonProperty("projectName")]
            public string? ProjectName { get; set; }

            [JsonProperty("projectUniqueName")]
            public string? ProjectUniqueName { get; set; }

            [JsonProperty("projectPath")]
            public string? ProjectPath { get; set; }

            [JsonProperty("packagesPath")]
            public string? PackagesPath { get; set; }

            [JsonProperty("outputPath")]
            public string? OutputPath { get; set; }

            [JsonProperty("projectStyle")]
            public string? ProjectStyle { get; set; }

            [JsonProperty("crossTargeting")]
            public string? CrossTargeting { get; set; }

            [JsonProperty("fallbackFolders")]
            public List<string>? FallbackFolders { get; set; }

            [JsonProperty("configFilePaths")]
            public List<string>? ConfigFilePaths { get; set; }

            [JsonProperty("originalTargetFrameworks")]
            public List<string>? OriginalTargetFrameworks { get; set; }

            [JsonProperty("sources")]
            public Dictionary<string, object>? Sources { get; set; }

            [JsonProperty("frameworks")]
            public Dictionary<string, FrameworkGroup>? Frameworks { get; set; }

            public sealed class FrameworkGroup
            {
                [JsonProperty("targetAlias")]
                public string? TargetAlias { get; set; }

                [JsonProperty("projectReferences")]
                public Dictionary<string, ProjectReference>? ProjectReferences { get; set; }

                public sealed class ProjectReference
                {
                    [JsonProperty("projectPath")]
                    public string? ProjectPath { get; set; }

                    [JsonProperty("includeAssets")]
                    public string? IncludeAssets { get; set; }

                    [JsonProperty("excludeAssets")]
                    public string? ExcludeAssets { get; set; }

                    [JsonProperty("privateAssets")]
                    public string? PrivateAssets { get; set; }
                }
            }
        }
    }
}