#if UNITY_EDITOR
using UnityEditor;
using System.IO;

public static class BuildScript
{
    // Build a development Windows standalone and place it under Builds/Windows/IsoSurvival
    [MenuItem("IsoSurvival/Build/Build Windows Development")]
    public static void BuildWindowsDevelopment()
    {
        string buildDir = "Builds/Windows/IsoSurvival";
        if (!Directory.Exists(buildDir)) Directory.CreateDirectory(buildDir);

        // Ensure sample scene is present in build settings
        string sampleScene = "Assets/Scenes/Sample.unity";
        if (!System.IO.File.Exists(sampleScene))
        {
            UnityEngine.Debug.LogWarning("Sample scene not found. Please use IsoSurvival > Create Sample Scene first.");
        }

        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[] { new EditorBuildSettingsScene(sampleScene, true) };
        EditorBuildSettings.scenes = scenes;

        string path = Path.Combine(buildDir, "IsoSurvival.exe");
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            scenes = new string[] { sampleScene },
            locationPathName = path,
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.Development | BuildOptions.AllowDebugging
        };

        var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            UnityEngine.Debug.Log($"Build succeeded: {path}");
        }
        else
        {
            UnityEngine.Debug.LogError($"Build failed: {report.summary.result}");
        }
    }
}
#endif