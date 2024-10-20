using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GlyphOnClickedPlane : MonoBehaviour
{
    [SerializeField] private GameObject glyph;  // Reference to your 2D element (Canvas with Images)
    [SerializeField] private Camera mainCamera;  // Reference to your main Camera
    [SerializeField] private GameObject PlaneFinder;  // Reference to the Ground Plane Stage (or a stable object)
    [SerializeField] private GameObject Canvas;  // Reference to the Ground Plane Stage (or a stable object)
    private HitTestResult previousHit;
    
    private void Start()
    {
        glyph.SetActive(false);

    }
    // This method captures the hit test result from the place finder
    public void IntersectionLocation(HitTestResult intersection)
    {
        if (intersection != null)
        {
            previousHit = intersection;
        }
    }


    public void CreatePlane()
    {
        glyph.SetActive(true);
        PlaneFinder.SetActive(false);
        Debug.LogWarning("createplane");
        if (previousHit != null)
        {
            // Instantiate the 2D Canvas prefab at the hit point
            //Vector3 hitPosition = previousHit.Position;

            // Set the Canvas position to the hit point
            //Canvas.transform.position = hitPosition;

            // Make the Canvas face the Camera
            //Canvas.transform.LookAt(mainCamera.transform);
        }
        else
        {
            Debug.LogWarning("No hit result available to place the canvas.");
        }

    }
}