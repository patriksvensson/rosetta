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

We're using [Cake](https://github.com/cake-build/cake) as a 
[dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) 
for building. So make sure that you've restored Cake by running 
the following in the repository root:

```
> dotnet tool restore
```

After that, running the build is as easy as writing:

```
> dotnet cake
```
