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
}
