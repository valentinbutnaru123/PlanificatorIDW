public static class Paths
{
    public static FilePath SolutionFile => "PlanificatorCMD.sln";
    public static string ObjPattern => "**/[Oo]bj";
    public static string BinPattern => "**/[Bb]in";
    public static string TestResultsPattern => "**/[Tt]est[Rr]esults";
    public static string ArtifactsPattern => "**/[Aa]rtifacts";
    public static DirectoryPath CoverageDir => "coverage";
    public static string CoveragePattern => "**/[Cc]overage";
    public static string TestProjectDirectory => "Planificator.Tests";
    public static FilePath CoverageFile => "coverage.xml";
}