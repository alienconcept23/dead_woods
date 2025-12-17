using UnityEngine;

// Simple smooth-follow camera for isometric projection.
public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 20, -20);
    public float smoothTime = 0.2f;

    public float minZoom = 10f;
    public float maxZoom = 40f;
    public float zoomSpeed = 10f;

    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Handle zoom input (mouse wheel) with clamp
        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(wheel) > 0.001f)
        {
            offset = offset + new Vector3(0, -wheel * zoomSpeed, wheel * zoomSpeed);
            offset.y = Mathf.Clamp(offset.y, minZoom, maxZoom);
            offset.z = -Mathf.Abs(offset.y);
        }

        Vector3 desired = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desired, ref _velocity, smoothTime);
        transform.LookAt(target.position);
    }
}
