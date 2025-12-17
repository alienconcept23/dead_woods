#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.IO;

public static class BuildScript
{
    // Build a development Windows standalone and place it under Builds/Windows/IsoSurvival
    [MenuItem("IsoSurvival/Build/Build Windows Development")]
    public static void BuildWindowsDevelopment()
    {
        string buildDir = "Builds/Windows/IsoSurvival";
        if (!Directory.Exists(buildDir)) Directory.CreateDirectory(buildDir);

        // Ensure sample scene is present in build settings; create it if missing
        string sampleScene = "Assets/Scenes/Sample.unity";
        if (!System.IO.File.Exists(sampleScene))
        {
            UnityEngine.Debug.Log("Sample scene not found. Creating sample scene...");
            var scene = UnityEditor.SceneManagement.EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

            // Create ground
            var ground = UnityEngine.GameObject.CreatePrimitive(PrimitiveType.Plane);
            ground.transform.localScale = new UnityEngine.Vector3(5, 1, 5);
            ground.name = "Ground";

            // Create player
            var player = new UnityEngine.GameObject("Player");
            player.transform.position = new UnityEngine.Vector3(0, 1, 0);
            player.tag = "Player";
            player.AddComponent<CharacterController>();
            // Attach PlayerController if available
            player.AddComponent<PlayerController>();
            // Add Health
            player.AddComponent<Health>();

            // Create camera
            var camGO = new UnityEngine.GameObject("Main Camera");
            var cam = camGO.AddComponent<UnityEngine.Camera>();
            camGO.tag = "MainCamera";
            var camController = camGO.AddComponent<CameraController>();
            camController.target = player.transform;
            camController.offset = new UnityEngine.Vector3(0, 20, -20);

            // Ensure directory exists
            var dir = System.IO.Path.GetDirectoryName(sampleScene);
            if (!System.IO.Directory.Exists(dir)) System.IO.Directory.CreateDirectory(dir);
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene, sampleScene);

            UnityEngine.Debug.Log("Sample scene created at " + sampleScene);
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