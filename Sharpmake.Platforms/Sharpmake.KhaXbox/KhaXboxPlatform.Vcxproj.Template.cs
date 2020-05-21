namespace Sharpmake
{
    public static partial class KhaXbox
    {
        public sealed partial class KhaXboxPlatform
        {
            private const string _projectConfigurationsTemplate =
                @"  <PropertyGroup Condition=""'$(Configuration)|$(Platform)'=='[conf.Name]|[platformName]'"">
    <IncludePath>[options.IncludePath]</IncludePath>
    <LibraryPath>[options.LibraryPath]</LibraryPath>
    <AdditionalOptions>[options.AdditionalOptions]</AdditionalOptions>
    <ExecutablePath>%SystemRoot%\system32;%SystemRoot%;%SystemRoot%\System32\Wbem;</ExecutablePath>
  </PropertyGroup>
";
        }
    }
}
