using UnityEngine;

// Simple snap-grid building prototype. Left-click to place the selected prefab snapped to the iso grid.
public class SnapGridBuilder : MonoBehaviour
{
    public GameObject placeablePrefab;
    public IsoTilemap tilemap; // used for snapping
    public LayerMask placementMask;

    void Update()
    {
        if (placeablePrefab == null || tilemap == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 100f, placementMask))
            {
                Vector3 local = transform.InverseTransformPoint(hit.point);
                var (ix, iy) = WorldToIsoCell(local);
                var pos = tilemap.IsoToWorld(ix, iy);
                Instantiate(placeablePrefab, pos + transform.position + Vector3.up * 0.5f, Quaternion.identity);
            }
        }
    }

    (int, int) WorldToIsoCell(Vector3 worldPos)
    {
        // Reverse of IsoToWorld basic approximation
        float tx = worldPos.x / (tilemap.tileWidth * 0.5f);
        float ty = worldPos.z / (tilemap.tileWidth * 0.25f);
        float x = (tx + ty) * 0.5f;
        float y = (ty - tx) * 0.5f;
        int ix = Mathf.RoundToInt(x);
        int iy = Mathf.RoundToInt(y);
        ix = Mathf.Clamp(ix, 0, tilemap.width-1);
        iy = Mathf.Clamp(iy, 0, tilemap.height-1);
        return (ix, iy);
    }
}