# Rosetta

_[![Rosetta NuGet Version](https://img.shields.io/nuget/v/rosetta.svg?style=flat&label=NuGet%3A%20Rosetta)](https://www.nuget.org/packages/rosetta)_

A strongly typed representation over `project.assets.json`.

## Examples

```csharp
// Load from JSON
var assets = AssetFile.FromJson(json);

// Load from stream
var assets = AssetFile.FromStream(stream);

// Load from file
var assets = AssetFile.FromFile("C:/foo/project.assets.json");
```

## Building

```
> dotnet build.cs
```
