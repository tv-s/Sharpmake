using Sharpmake;
using System.IO;

[module: Sharpmake.Include("baseclasses.sharpmake.cs")]
[module: Sharpmake.Include("externprojects.sharpmake.cs")]
[module: Sharpmake.Include("projects.sharpmake.cs")]


namespace VCPKGSample
{
    public static class Main
    {
        [Sharpmake.Main]
        public static void SharpmakeMain(Sharpmake.Arguments arguments)
        {

            FileInfo sharpmakeFileInfo = Util.GetCurrentSharpmakeFileInfo();
            string sharpmakeFileDirectory = Util.PathMakeStandard(sharpmakeFileInfo.DirectoryName);
            string absoluteRootPath = Util.PathGetAbsolute(sharpmakeFileDirectory, @"..\tmp");

            // This is necessary since there is no rc.exe in the same directory than link.exe
            FastBuildSettings.SetPathToResourceCompilerInEnvironment = true;
            FastBuildSettings.FastBuildMakeCommand = Util.PathGetAbsolute(sharpmakeFileDirectory, @"..\..\..\tools\FastBuild\FBuild.exe");

            Util.FilesAutoCleanupDBPath = absoluteRootPath;
			Util.FilesAutoCleanupActive = true;
            arguments.Generate<VCPKGSampleSolution>();


        }
    }
}