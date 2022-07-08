namespace Rosetta;

[Flags]
public enum IncludeFlags : ushort
{
    None = 0,
    Runtime = 1 << 0,
    Compile = 1 << 1,
    Build = 1 << 2,
    Native = 1 << 3,
    ContentFiles = 1 << 4,
    Analyzers = 1 << 5,
    BuildTransitive = 1 << 6,
    All = Analyzers | Build | Compile | ContentFiles | Native | Runtime | BuildTransitive,
}
