#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public static class SceneSetup
{
    [MenuItem("IsoSurvival/Create Sample Scene")]
    public static void CreateSampleScene()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        // Create ground
        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.transform.localScale = new Vector3(5, 1, 5);
        ground.name = "Ground";

        // Create player
        var player = new GameObject("Player");
        player.transform.position = new Vector3(0, 1, 0);
        player.tag = "Player";
        player.AddComponent<CharacterController>();
        // Attach PlayerController if available
        player.AddComponent<PlayerController>();

        // Add Health
        player.AddComponent<Health>();

        // Create camera
        var camGO = new GameObject("Main Camera");
        var cam = camGO.AddComponent<Camera>();
        camGO.tag = "MainCamera";
        var camController = camGO.AddComponent<CameraController>();
        camController.target = player.transform;
        camController.offset = new Vector3(0, 20, -20);

        // Save scene
        EditorSceneManager.SaveScene(scene, "Assets/Scenes/Sample.unity");

        Debug.Log("Sample scene created at Assets/Scenes/Sample.unity");
    }
}
#endif