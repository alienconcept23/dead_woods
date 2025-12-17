using UnityEngine;

// Simple isometric tilemap generator for prototyping.
// This creates a grid of quads textured with placeholder tiles and positions them in isometric space.
[ExecuteAlways]
public class IsoTilemap : MonoBehaviour
{
    public int width = 16;
    public int height = 16;
    public float tileWidth = 1f; // world units per 64px tile (tweak in-editor)
    public Material tileMaterial; // assign a simple material with placeholder texture

    public void Generate()
    {
        Clear();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = IsoToWorld(x, y);
                var go = GameObject.CreatePrimitive(PrimitiveType.Quad);
                go.transform.SetParent(transform, false);
                go.transform.localPosition = pos;
                go.transform.localRotation = Quaternion.Euler(90, 0, 0); // face upward
                go.transform.localScale = Vector3.one * tileWidth;
                if (tileMaterial != null) go.GetComponent<Renderer>().sharedMaterial = tileMaterial;
                go.name = $"Tile_{x}_{y}";
            }
        }
    }

    public void Clear()
    {
        var children = new System.Collections.Generic.List<Transform>();
        foreach (Transform t in transform) children.Add(t);
        foreach (var t in children) { if (Application.isPlaying) Destroy(t.gameObject); else DestroyImmediate(t.gameObject); }
    }

    public Vector3 IsoToWorld(int x, int y)
    {
        // Simple isometric mapping (diamond layout)
        float worldX = (x - y) * (tileWidth * 0.5f);
        float worldZ = (x + y) * (tileWidth * 0.25f);
        return new Vector3(worldX, 0f, worldZ);
    }

    // Editor helper
#if UNITY_EDITOR
    [UnityEditor.MenuItem("IsoSurvival/Generate Demo Tilemap")]
    public static void CreateDemo()
    {
        var obj = new GameObject("IsoTilemap");
        var tm = obj.AddComponent<IsoTilemap>();
        tm.width = 16;
        tm.height = 16;
        // Leave material null; user should assign a placeholder material.
        UnityEditor.Selection.activeGameObject = obj;
    }
#endif
}