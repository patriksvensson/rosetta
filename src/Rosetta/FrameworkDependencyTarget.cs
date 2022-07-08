namespace Rosetta;

public enum FrameworkDependencyTarget
{
    None = 0,
    Package = 1 << 0,
    Project = 1 << 1,
    ExternalProject = 1 << 2,
    Assembly = 1 << 3,
    Reference = 1 << 4,
    WinMD = 1 << 5,
    All = Package | Project | ExternalProject | Assembly | Reference | WinMD,
    PackageProjectExternal = Package | Project | ExternalProject,
}