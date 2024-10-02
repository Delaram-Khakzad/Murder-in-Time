using UnityEngine;
using Vuforia;

public class CustomObserverEventHandler : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    public GameObject[] objectsToShowOrHide;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged += OnTrackableStateChanged;
        }
    }

    private void OnTrackableStateChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            SetObjectsVisibility(true);
        }
        else
        {
            SetObjectsVisibility(false);
        }
    }

    private void SetObjectsVisibility(bool visible)
    {
        foreach (var obj in objectsToShowOrHide)
        {
            obj.SetActive(visible);
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTrackableStateChanged;
        }
    }
}