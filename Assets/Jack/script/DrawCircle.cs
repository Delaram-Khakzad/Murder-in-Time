using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour
{
    public float radius = 1.0f;       // Radius of the circle
    public int segments = 100;        // Number of segments for smoothness
    private LineRenderer lineRenderer;

    void Start()
    {
        // Get or add the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Set LineRenderer properties for a clear edge
        lineRenderer.positionCount = segments + 1; // +1 to complete the loop
        lineRenderer.useWorldSpace = false; // Optional, can use local space for easy transformations
        lineRenderer.loop = true; // Close the loop to make a continuous edge

        // Set width for the visible edge
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        // Draw the edge of the circle
        DrawCircleEdgeOnly();
    }

    void DrawCircleEdgeOnly()
    {
        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            // Calculate x and z coordinates for the circle edge (on the XZ plane)
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            // Set each point along the edge
            lineRenderer.SetPosition(i, new Vector3(x, 0, z));

            // Increment the angle for the next point on the edge
            angle += 2 * Mathf.PI / segments;
        }
    }
}
