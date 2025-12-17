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

    // Editor render-based capture (doesn't require entering play mode). More reliable for automated captures.
    public static void RunEditorCapture()
    {
        var scenePath = "Assets/Scenes/Sample.unity";
        if (!File.Exists(scenePath))
        {
            Debug.LogError("Sample scene not found: " + scenePath);
            return;
        }

        EditorSceneManager.OpenScene(scenePath);

        var outDir = Path.Combine(Application.dataPath, "../Captures");
        Directory.CreateDirectory(outDir);

        Camera cam = Camera.main;
        bool createdTempCam = false;
        if (cam == null)
        {
            // try find any camera
            cam = Object.FindObjectOfType<Camera>();
        }
        if (cam == null)
        {
            // create a temporary camera
            var go = new GameObject("_TempCaptureCam");
            cam = go.AddComponent<Camera>();
            cam.transform.position = new Vector3(0, 10, -10);
            cam.transform.LookAt(Vector3.zero);
            createdTempCam = true;
        }

        int width = 800, height = 600;
        RenderTexture rt = new RenderTexture(width, height, 24);
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        for (int i = 0; i < 10; i++)
        {
            // slight camera jitter for multiple frames
            cam.transform.position += new Vector3(0.1f * i, 0, 0);
            cam.targetTexture = rt;
            RenderTexture.active = rt;
            cam.Render();
            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();
            byte[] bytes = tex.EncodeToPNG();
            string fname = Path.Combine(outDir, $"editor_capture_{i:D3}.png");
            File.WriteAllBytes(fname, bytes);
            Debug.Log("Saved editor capture: " + fname);
            cam.targetTexture = null;
            RenderTexture.active = null;
        }

        Object.DestroyImmediate(rt);
        Object.DestroyImmediate(tex);
        if (createdTempCam) Object.DestroyImmediate(cam.gameObject);

        Debug.Log($"Editor capture complete. Images saved to {outDir}");
    }

    static double _stopTime = 0;
    static int _frame = 0;
    static string _outDir;
    static bool _started = false;
}
