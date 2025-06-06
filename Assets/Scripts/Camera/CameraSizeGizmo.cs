using UnityEngine;

public class CameraSizeGizmo : MonoBehaviour
{
    [Header("Camera Gizmo Settings")]
    public Color gizmoColor = Color.cyan;
    public bool showOnlyWhenSelected = false;

    private Camera cam;

    private void Start()
    {
        // Get the camera component (either on this object or the main camera)
        cam = GetComponent<Camera>();
        if (cam == null)
            cam = Camera.main;
    }

    private void OnDrawGizmos()
    {
        if (!showOnlyWhenSelected)
            DrawCameraGizmo();
    }

    private void OnDrawGizmosSelected()
    {
        if (showOnlyWhenSelected)
            DrawCameraGizmo();
    }

    private void DrawCameraGizmo()
    {
        // Get camera reference if not set
        if (cam == null)
        {
            cam = GetComponent<Camera>();
            if (cam == null)
                cam = Camera.main;
        }

        if (cam == null) return;

        // Calculate camera bounds in world space
        float cameraHeight = cam.orthographicSize * 2f;
        float cameraWidth = cameraHeight * cam.aspect;

        Vector3 cameraPosition = transform.position;

        // Set gizmo color
        Gizmos.color = gizmoColor;

        // Draw wireframe rectangle representing camera view
        Vector3 topLeft = cameraPosition + new Vector3(-cameraWidth / 2f, cameraHeight / 2f, 0f);
        Vector3 topRight = cameraPosition + new Vector3(cameraWidth / 2f, cameraHeight / 2f, 0f);
        Vector3 bottomLeft = cameraPosition + new Vector3(-cameraWidth / 2f, -cameraHeight / 2f, 0f);
        Vector3 bottomRight = cameraPosition + new Vector3(cameraWidth / 2f, -cameraHeight / 2f, 0f);

        // Draw the four sides of the rectangle
        Gizmos.DrawLine(topLeft, topRight);     // Top
        Gizmos.DrawLine(topRight, bottomRight); // Right
        Gizmos.DrawLine(bottomRight, bottomLeft); // Bottom
        Gizmos.DrawLine(bottomLeft, topLeft);   // Left

        // Optional: Draw diagonals for better visibility
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, 0.3f);
        Gizmos.DrawLine(topLeft, bottomRight);
        Gizmos.DrawLine(topRight, bottomLeft);
    }
}