using UnityEngine;

// Simple snap-grid building prototype. Left-click to place the selected prefab snapped to the iso grid.
public class SnapGridBuilder : MonoBehaviour
{
    public GameObject placeablePrefab;
    public IsoTilemap tilemap; // used for snapping
    public LayerMask placementMask;
    public GameObject previewPrefab;

    GameObject _previewInstance;
    System.Collections.Generic.HashSet<(int,int)> _occupied = new System.Collections.Generic.HashSet<(int,int)>();

    void Update()
    {
        if (placeablePrefab == null || tilemap == null) return;

        UpdatePreview();

        if (Input.GetMouseButtonDown(0))
        {
            if (TryGetMouseIsoCell(out int ix, out int iy, out Vector3 pos))
            {
                if (!_occupied.Contains((ix, iy)))
                {
                    Instantiate(placeablePrefab, pos + transform.position + Vector3.up * 0.5f, Quaternion.identity);
                    _occupied.Add((ix, iy));
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // Right click: remove object at cell if present
            if (TryGetMouseIsoCell(out int ix, out int iy, out Vector3 pos))
            {
                Collider[] hits = Physics.OverlapSphere(pos + transform.position + Vector3.up * 0.5f, 0.5f);
                foreach (var c in hits)
                {
                    if (c.gameObject.CompareTag("Buildable"))
                    {
                        Destroy(c.gameObject);
                        _occupied.Remove((ix, iy));
                    }
                }
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

    void UpdatePreview()
    {
        if (previewPrefab == null) return;

        if (_previewInstance == null)
        {
            _previewInstance = Instantiate(previewPrefab, Vector3.zero, Quaternion.identity);
            foreach (var r in _previewInstance.GetComponentsInChildren<Renderer>())
            {
                r.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                r.receiveShadows = false;
                r.material = new Material(r.sharedMaterial) { color = new Color(1f,1f,1f,0.6f) };
            }
        }

        if (TryGetMouseIsoCell(out int ix, out int iy, out Vector3 pos))
        {
            _previewInstance.SetActive(true);
            _previewInstance.transform.position = pos + transform.position + Vector3.up * 0.5f;
            _previewInstance.transform.rotation = Quaternion.identity;
            bool valid = !_occupied.Contains((ix, iy));
            SetPreviewColor(valid ? Color.green : Color.red);
        }
        else
        {
            if (_previewInstance) _previewInstance.SetActive(false);
        }
    }

    void SetPreviewColor(Color c)
    {
        var rs = _previewInstance.GetComponentsInChildren<Renderer>();
        foreach (var r in rs)
        {
            if (r.material.HasProperty("_Color")) r.material.color = new Color(c.r, c.g, c.b, 0.6f);
        }
    }

    bool TryGetMouseIsoCell(out int ix, out int iy, out Vector3 pos)
    {
        ix = iy = -1; pos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 100f, placementMask))
        {
            Vector3 local = transform.InverseTransformPoint(hit.point);
            (ix, iy) = WorldToIsoCell(local);
            pos = tilemap.IsoToWorld(ix, iy);
            return true;
        }
        return false;
    }
}
