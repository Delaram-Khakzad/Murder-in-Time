using UnityEngine;
using Vuforia;

public class PersistentObjectsManager : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    private bool isTargetVisible = false;

    // Arrays for the objects to control
    public GameObject[] objects3D;   // Reference to 3D objects (e.g., models)
    
    private Vector3[] originalPositions; // Store the original positions of objects
    private Quaternion[] originalRotations; // Store the original rotations of objects

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            // Register the event handler
            observerBehaviour.OnTargetStatusChanged += OnTrackableStateChanged;
        }

        // Store original positions and rotations of objects
        originalPositions = new Vector3[objects3D.Length];
        originalRotations = new Quaternion[objects3D.Length];

        for (int i = 0; i < objects3D.Length; i++)
        {
            if (objects3D[i] != null)
            {
                originalPositions[i] = objects3D[i].transform.position;
                originalRotations[i] = objects3D[i].transform.rotation;
            }
        }

        // Optionally start with the objects hidden
        SetObjectsVisibility(false); // Set them initially hidden until the target is tracked
    }

    private void OnTrackableStateChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            // When the target is tracked or extended tracked, make objects visible
            OnTrackingFound();
        }
        else
        {
            // When the target is lost, but we want the objects to remain visible
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        // The image target is found, make all objects (UI and 3D) visible
        isTargetVisible = true;
        SetObjectsVisibility(true);
        UpdateObjectPositions();
    }

    private void OnTrackingLost()
    {
        // Keep objects visible even if the target is lost (we're mimicking extended tracking behavior)
        if (isTargetVisible)
        {
            SetObjectsVisibility(true);  // Keep them visible
        }
    }

    private void SetObjectsVisibility(bool visible)
    {
        // Set visibility for all 3D objects
        foreach (GameObject object3D in objects3D)
        {
            if (object3D != null)
            {
                object3D.SetActive(visible);
            }
        }
    }

    private void UpdateObjectPositions()
{
    // Set objects to their original world position and rotation
    for (int i = 0; i < objects3D.Length; i++)
    {
        if (objects3D[i] != null)
        {
            // Instead of setting to original position, make sure they are positioned in world space
            // This prevents them from moving relative to the image target or camera
            objects3D[i].transform.position = originalPositions[i];  // Ensure these positions are in world space
            objects3D[i].transform.rotation = originalRotations[i];  // Ensure these rotations are in world space
        }
    }
}

    void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            // Unregister the event handler
            observerBehaviour.OnTargetStatusChanged -= OnTrackableStateChanged;
        }
    }
}
