using System.CodeDom;
using System.Collections.Generic;
using Sharpmake.Generators;
using Sharpmake.Generators.VisualStudio;

namespace Sharpmake
{
    public static partial class KhaXbox
    {
        [PlatformImplementation(Platform.xbox,
            typeof(IPlatformDescriptor),
            typeof(Project.Configuration.IConfigurationTasks),
            typeof(IPlatformVcxproj))]
        public sealed partial class KhaXboxPlatform : BaseMicrosoftPlatform
        {
            #region IPlatformDescriptor
            public override string SimplePlatformString => "KhaXbox";
            public override bool HasDotNetSupport => false;
            #endregion

            #region IPlatformVcxproj implementation
            public override string PackageFileExtension => "xbe";
            public override bool IsPcPlatform => false;

            /*public override IEnumerable<string> GetImplicitlyDefinedSymbols(IGenerationContext context)
            {
                yield return "";
            }*/

            public override IEnumerable<string> GetLibraryPaths(IGenerationContext context)
            {
                yield return GetLibPath();
            }

            public override void SetupPlatformToolsetOptions(IGenerationContext context)
            {
                context.Options["PlatformToolset"] = FileGeneratorUtilities.RemoveLineTag;
            }

            public override void SelectCompilerOptions(IGenerationContext context)
            {
                context.Options["IncludePath"] = GetIncludePath();
                context.Options["LibraryPath"] = GetLibPath();
                var additionalOptions = new List<string>();
                context.SelectOption
                (
                Sharpmake.Options.Option(Sharpmake.Options.Vc.Compiler.CLanguageStandard.C11, () => { additionalOptions.Add("/std:c11"); }),
                Sharpmake.Options.Option(Sharpmake.Options.Vc.Compiler.CLanguageStandard.C17, () => { additionalOptions.Add("/std:c17"); })
                );
                context.SelectOption
                (
                Sharpmake.Options.Option(Sharpmake.Options.Vc.Compiler.CppLanguageStandard.CPP17, () => { additionalOptions.Add("/std:c++17"); })
                );
                context.Options["AdditionalOptions"] = string.Join(" ", additionalOptions.ToArray());
            }

            public override void SelectLinkerOptions(IGenerationContext context)
            {
            }

            public override void GenerateProjectConfigurationGeneral2(IVcxprojGenerationContext context, IFileGenerator generator)
            {
            }

            public override void GenerateProjectCompileVcxproj(IVcxprojGenerationContext context, IFileGenerator generator)
            {
                
            }

            public override void GenerateProjectConfigurationCustomMakeFile(IVcxprojGenerationContext context, IFileGenerator generator)
            {
                base.GenerateProjectConfigurationCustomMakeFile(context, generator);
                generator.Write(_projectConfigurationsTemplate);
            }

            protected override string GetProjectStaticLinkVcxprojTemplate()
            {
                return "";
            }

            protected override string GetProjectLinkSharedVcxprojTemplate()
            {
                return "";
            }

            protected override IEnumerable<string> GetIncludePathsImpl(IGenerationContext context)
            {
                var dirs = new List<string>() { GetIncludePath() };
                foreach (var i in context.Configuration.IncludePaths)
                    dirs.Add(i);
                foreach (var i in context.Configuration.DependenciesIncludePaths)
                    dirs.Add(i);
                foreach (var i in context.Configuration.IncludePrivatePaths)
                    dirs.Add(i);
                return dirs;
            }

            #endregion
        }

        public static string GetIncludePath()
        {
            var xdkPath = System.IO.Path.GetFullPath(System.Environment.GetEnvironmentVariable("KHA_XBOX_SDK"));
            return System.IO.Path.Combine(xdkPath, @"XDK\xbox\include");
        }

        public static string GetLibPath()
        {
            var xdkPath = System.IO.Path.GetFullPath(System.Environment.GetEnvironmentVariable("KHA_XBOX_SDK"));
            return System.IO.Path.Combine(xdkPath, @"XDK\xbox\lib");
        }
    }
}
