using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using System.Collections;
using System.IO;

// Simple editor utility to run the sample scene for a short time and capture screenshots.
// Usage: in batch mode: -executeMethod PlaymodeCapture.RunCapture
public class PlaymodeCapture
{
    public static void RunCapture()
    {
        // Blocking capture flow to ensure Unity stays alive during capture when invoked via -executeMethod
        var scenePath = "Assets/Scenes/Sample.unity";
        if (!File.Exists(scenePath))
        {
            Debug.LogError("Sample scene not found: " + scenePath);
            return;
        }

        EditorSceneManager.OpenScene(scenePath);

        // Create output dir
        var outDir = Path.Combine(Application.dataPath, "../Captures");
        Directory.CreateDirectory(outDir);

        Debug.Log("Entering play mode for short capture...");
        EditorApplication.isPlaying = true;

        double stopTime = EditorApplication.timeSinceStartup + 3.0; // 3 seconds
        int frame = 0;
        // Capture at ~10 FPS
        while (EditorApplication.isPlaying && EditorApplication.timeSinceStartup < stopTime)
        {
            string fname = Path.Combine(outDir, $"capture_{frame:D3}.png");
            ScreenCapture.CaptureScreenshot(fname);
            Debug.Log("Captured: " + fname);
            frame++;
            System.Threading.Thread.Sleep(100); // ~10 FPS
        }

        if (EditorApplication.isPlaying)
        {
            Debug.Log("Stop requested: leaving play mode.");
            EditorApplication.isPlaying = false;
        }

        Debug.Log($"Capture complete, {frame} frames saved to {outDir}");
    }

    static double _stopTime = 0;
    static int _frame = 0;
    static string _outDir;
    static bool _started = false;
}
