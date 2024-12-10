using UnityEngine;
using Vuforia;

public class EnableGravityOnTargetTracking : MonoBehaviour
{
    // Reference to the Rigidbody of the object to enable gravity
    public Rigidbody targetRigidbody;

    // Reference to the other Image Target being tracked
    public GameObject otherImageTarget;

    private ObserverBehaviour observerBehaviour;

    private void Start()
    {
        // Ensure the target Rigidbody is assigned
        if (targetRigidbody == null)
        {
            Debug.LogError("Target Rigidbody not assigned. Please assign it in the inspector.");
            return;
        }

        // Ensure the other image target is assigned
        if (otherImageTarget == null)
        {
            Debug.LogError("Other Image Target not assigned. Please assign it in the inspector.");
            return;
        }

        // Get the ObserverBehaviour of the other image target
        observerBehaviour = otherImageTarget.GetComponent<ObserverBehaviour>();

        if (observerBehaviour == null)
        {
            Debug.LogError("ObserverBehaviour not found on the other Image Target. Ensure it is a Vuforia Image Target.");
            return;
        }

        // Disable gravity at the start
        targetRigidbody.useGravity = false;

        // Subscribe to tracking status changes
        observerBehaviour.OnTargetStatusChanged += OnOtherTargetStatusChanged;
    }

    private void OnDestroy()
    {
        // Unsubscribe from tracking status changes
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnOtherTargetStatusChanged;
        }
    }

    private void OnOtherTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Enable gravity if the other target is being tracked
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            targetRigidbody.useGravity = true;
        }
        else
        {
            targetRigidbody.useGravity = false;
        }
    }
}
