using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.Tilemaps;
using System.IO;

// Generates simple placeholder sprites, tiles and prefabs for quick prototyping.
public static class GeneratePlaceholderArt
{
    [MenuItem("Tools/Generate Placeholder Art")]
    public static void GenerateAll()
    {
        Debug.Log("Generating placeholder art and tilemap...");
        string spritesDir = "Assets/Sprites";
        string tilesDir = "Assets/Tiles";
        string prefabsDir = "Assets/Prefabs";

        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Sprites"));
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Tiles"));
        Directory.CreateDirectory(Path.Combine(Application.dataPath, "Prefabs"));

        // Create simple sprites (name -> color)
        var items = new (string name, Color col)[] {
            ("ground", new Color(0.6f,0.45f,0.25f)),
            ("grass", new Color(0.3f,0.7f,0.3f)),
            ("player", Color.cyan),
            ("enemy", Color.magenta),
            ("tree", new Color(0.1f,0.5f,0.1f)),
            ("rock", new Color(0.4f,0.4f,0.45f))
        };

        foreach (var it in items)
        {
            string texPath = $"Assets/Sprites/{it.name}.png";
            CreateColoredTexture(texPath, 64, 64, it.col, it.name == "player" ? Color.white : (Color?)null);
        }

        AssetDatabase.Refresh();

        // Set importers to Sprite
        foreach (var it in items)
        {
            string assetPath = $"Assets/Sprites/{it.name}.png";
            var ti = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (ti != null)
            {
                ti.textureType = TextureImporterType.Sprite;
                ti.spritePixelsPerUnit = 100;
                ti.SaveAndReimport();
            }
        }

        AssetDatabase.Refresh();

        // Create tiles for ground and grass
        CreateTileFromSprite("ground");
        CreateTileFromSprite("grass");

        // Create prefabs for tree, rock, player, enemy
        CreatePrefabFromSprite("tree", true);
        CreatePrefabFromSprite("rock", true);
        CreatePrefabFromSprite("player", false);
        CreatePrefabFromSprite("enemy", false);

        // Open sample scene and place a simple tilemap
        string scenePath = "Assets/Scenes/Sample.unity";
        if (File.Exists(scenePath))
        {
            var scene = EditorSceneManager.OpenScene(scenePath);
            // find or create a Grid + Tilemap
            GameObject gridGO = GameObject.Find("Grid");
            if (gridGO == null)
            {
                gridGO = new GameObject("Grid");
                gridGO.AddComponent<Grid>();
            }
            var tilemapGO = GameObject.Find("Tilemap");
            Tilemap tilemap;
            if (tilemapGO == null)
            {
                tilemapGO = new GameObject("Tilemap", typeof(Tilemap), typeof(TilemapRenderer));
                tilemapGO.transform.parent = gridGO.transform;
                tilemap = tilemapGO.GetComponent<Tilemap>();
            }
            else tilemap = tilemapGO.GetComponent<Tilemap>();

            // load tiles
            Tile groundTile = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tiles/ground.tile");
            Tile grassTile = AssetDatabase.LoadAssetAtPath<Tile>("Assets/Tiles/grass.tile");

            // fill area and add some grass patches
            for (int x = -10; x <= 10; x++)
            {
                for (int y = -10; y <= 10; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
            }
            // add some grass clumps
            for (int i = 0; i < 50; i++)
            {
                int x = Random.Range(-9, 10);
                int y = Random.Range(-9, 10);
                tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
            }

            // scatter a few prefabs (trees/rocks)
            var treePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/tree.prefab");
            var rockPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/rock.prefab");
            for (int i = 0; i < 30; i++)
            {
                int x = Random.Range(-9, 10);
                int y = Random.Range(-9, 10);
                Vector3 worldPos = new Vector3(x + 0.5f, y + 0.5f, 0f);
                if (Random.value > 0.5f && treePrefab != null)
                    PrefabUtility.InstantiatePrefab(treePrefab, scene) as GameObject;
                else if (rockPrefab != null)
                    PrefabUtility.InstantiatePrefab(rockPrefab, scene) as GameObject;
            }

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log("Tilemap and prefabs placed in Sample scene.");
        }
        else
        {
            Debug.LogWarning("Sample scene not found, skipping tilemap placement.");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Placeholder art generation complete.");
    }

    static void CreateTileFromSprite(string spriteName)
    {
        string spritePath = $"Assets/Sprites/{spriteName}.png";
        Sprite spr = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        if (spr == null) { Debug.LogWarning($"Sprite not found: {spritePath}"); return; }
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = spr;
        string tilePath = $"Assets/Tiles/{spriteName}.tile";
        AssetDatabase.CreateAsset(tile, tilePath);
    }

    static void CreatePrefabFromSprite(string spriteName, bool isBuildable)
    {
        string spritePath = $"Assets/Sprites/{spriteName}.png";
        Sprite spr = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        if (spr == null) { Debug.LogWarning($"Sprite not found: {spritePath}"); return; }

        var go = new GameObject(spriteName);
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = spr;
        if (isBuildable)
        {
            go.tag = "Buildable";
            var col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = false;
        }

        string prefabPath = $"Assets/Prefabs/{spriteName}.prefab";
        PrefabUtility.SaveAsPrefabAsset(go, prefabPath);
        Object.DestroyImmediate(go);
    }

    static void CreateColoredTexture(string path, int w, int h, Color color, Color? accent = null)
    {
        Texture2D tex = new Texture2D(w, h, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[w * h];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = color;

        if (accent.HasValue)
        {
            // draw a simple circle accent in center
            Color ac = accent.Value;
            int cx = w / 2; int cy = h / 2; int r = Mathf.Min(w, h) / 4;
            for (int y = 0; y < h; y++) for (int x = 0; x < w; x++)
            {
                int dx = x - cx; int dy = y - cy;
                if (dx * dx + dy * dy <= r * r)
                    pixels[y * w + x] = ac;
            }
        }

        tex.SetPixels(pixels);
        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
    }
}
