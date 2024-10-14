using UnityEngine;
using Vuforia;

public class SceneAlignmentManager : MonoBehaviour
{
    public GameObject sceneRoot; // Assign your SceneRoot GameObject here
    private ObserverBehaviour observerBehaviour;
    private bool isAligned = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Check if the image is being tracked and alignment hasn't been done yet
        if ((targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED) && !isAligned)
        {
            AlignScene();
            isAligned = true;
        }
    }

    private void AlignScene()
    {
        // Get the image target's position and rotation
        Vector3 targetPosition = transform.position;
        Quaternion targetRotation = transform.rotation;

        // Calculate the offset between the image target and the origin
        Vector3 positionOffset = -targetPosition;
        Quaternion rotationOffset = Quaternion.Inverse(targetRotation);

        // Apply the offsets to the sceneRoot
        sceneRoot.transform.position += positionOffset;
        sceneRoot.transform.rotation = rotationOffset * sceneRoot.transform.rotation;

        // Optional: If you want the scene to move with the image target after alignment, parent it to the image target
        // sceneRoot.transform.SetParent(transform, true); // 'true' keeps the world position and rotation
    }

    void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
