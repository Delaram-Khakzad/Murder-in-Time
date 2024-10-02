using UnityEngine;
using Vuforia;

public class WorldAlignmentManager : MonoBehaviour
{
    public GameObject sceneRoot; // Assign SceneRoot here
    private ObserverBehaviour observerBehaviour;
    private bool isAligned = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            // Register the event handler for image tracking
            observerBehaviour.OnTargetStatusChanged += OnTrackableStateChanged;
        }
    }

    private void OnTrackableStateChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // If the image is being tracked and alignment hasn't been done yet
        if ((targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED) && !isAligned)
        {
            // Align the world
            AlignWorld();
            isAligned = true;
        }
    }

    private void AlignWorld()
    {
        // Get the image target's position and rotation
        Vector3 targetPosition = transform.position;
        Quaternion targetRotation = transform.rotation;

        // Invert the image target's position and rotation
        Vector3 inversePosition = -targetPosition;
        Quaternion inverseRotation = Quaternion.Inverse(targetRotation);

        // Apply the inverse position and rotation to the scene root
        sceneRoot.transform.position = inversePosition;
        sceneRoot.transform.rotation = inverseRotation;

        // Optional: If you want the objects to stay aligned even when the image target moves
        // sceneRoot.transform.SetParent(transform);
    }

    void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            // Unregister the event handler when the object is destroyed
            observerBehaviour.OnTargetStatusChanged -= OnTrackableStateChanged;
        }
    }
}
