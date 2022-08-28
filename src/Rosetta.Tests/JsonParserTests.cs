using System.Linq;
using NuGet.Versioning;
using Shouldly;

namespace Rosetta.Tests;

public sealed class JsonParserTests
{
    [Theory]
    [InlineData("DotnetLibrary.json")]
    [InlineData("DotnetWebApp.json")]
    [InlineData("DotnetTest.json")]
    public void Should_Parse_Project_Json_File(string file)
    {
        // Given
        var input = EmbeddedResourceReader.Read($"Rosetta.Tests/Data/{file}");

        // When
        var model = AssetFile.FromJson(input);

        // Then
        model.ShouldNotBeNull();
    }

    [Fact]
    public void Should_Parse_Download_Dependencies()
    {
        // Given
        var input = EmbeddedResourceReader.Read("Rosetta.Tests/Data/DotnetLibrary.json");

        // When
        var model = AssetFile.FromJson(input);

        // Then
        model.Project.Frameworks.ShouldContain(x => x.Name == "net6.0");
        model.Project.Frameworks.ShouldContain(x => x.Name == "net5.0");
        model.Project.Frameworks.ShouldContain(x => x.Name == "netstandard2.0");

        var net6Target = model.Project.Frameworks.Single(x => x.Name == "net6.0");
        net6Target.DownloadDependencies.ShouldSatisfyAllConditions(
            x => x.Count.ShouldBe(1),
            x => x.Single().Name.ShouldBe("Microsoft.NETCore.App.Ref"),
            x => x.Single().VersionRange.ShouldBe(VersionRange.Parse("[3.0.0, 3.0.0]")));

        var net5Target = model.Project.Frameworks.Single(x => x.Name == "net5.0");
        net5Target.DownloadDependencies.ShouldSatisfyAllConditions(
            x => x.Count.ShouldBe(1),
            x => x.Single().Name.ShouldBe("Microsoft.NETCore.App.Ref"),
            x => x.Single().VersionRange.ShouldBe(VersionRange.Parse("[3.0.0, 3.0.0]")));

        var netstandardTarget = model.Project.Frameworks.Single(x => x.Name == "netstandard2.0");
        netstandardTarget.DownloadDependencies.ShouldSatisfyAllConditions(
            x => x.Count.ShouldBe(1),
            x => x.Single().Name.ShouldBe("Microsoft.NETCore.App.Ref"),
            x => x.Single().VersionRange.ShouldBe(VersionRange.Parse("[3.0.0, 3.0.0]")));
    }

    [Fact]
    public void Should_Parse_Empty_Download_Dependencies()
    {
        // Given
        var input = EmbeddedResourceReader.Read("Rosetta.Tests/Data/DotnetWebApp.json");

        // When
        var model = AssetFile.FromJson(input);

        // Then
        model.Project.Frameworks.ShouldHaveSingleItem();
        var framework = model.Project.Frameworks.Single();
        framework.DownloadDependencies.ShouldNotBeNull();
        model.Project.Frameworks.Single().DownloadDependencies.ShouldBeEmpty();
    }
}
