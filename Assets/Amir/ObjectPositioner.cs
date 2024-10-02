using UnityEngine;
using Vuforia;

public class ObjectPositioner : MonoBehaviour
{
    public GameObject[] objectsToPosition;  // Array of objects to activate and position
    private ObserverBehaviour observerBehaviour;
    private bool isPositioned = false;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            // Subscribe to the tracking state change event
            observerBehaviour.OnTargetStatusChanged += OnTrackableStateChanged;
        }

        // Deactivate objects at the start
        foreach (var obj in objectsToPosition)
        {
            obj.SetActive(false);
        }
    }

    private void OnTrackableStateChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Check if the target is tracked or extended tracked, and if objects are not yet positioned
        if ((targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED) && !isPositioned)
        {
            PositionObjects();
            isPositioned = true;
        }
    }

    private void PositionObjects()
    {
        // Activate and position each object relative to the target
        foreach (var obj in objectsToPosition)
        {
            if (obj != null)
            {
                // Activate the object
                obj.SetActive(true);

                // Set the object's position and rotation relative to the observer's position
                obj.transform.position = transform.position + obj.transform.position;
                obj.transform.rotation = transform.rotation * obj.transform.rotation;
            }
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTrackableStateChanged;
        }
    }
}
