using UnityEngine;
using UnityEngine.Events;
using Vuforia;

/// <summary>
/// A custom event handler that can be attached to image targets 
/// without modifying the default observer event handler.
/// </summary>
public class CustomTrackableEventHandler : MonoBehaviour
{
    public enum TrackingStatusFilter
    {
        Tracked,
        Tracked_ExtendedTracked,
        Tracked_ExtendedTracked_Limited
    }

    /// <summary>
    /// A filter that determines when the target should be considered visible
    /// </summary>
    public TrackingStatusFilter StatusFilter = TrackingStatusFilter.Tracked_ExtendedTracked_Limited;

    public UnityEvent OnTargetFound = new UnityEvent();
    public UnityEvent OnTargetLost = new UnityEvent();

    protected ObserverBehaviour mObserverBehaviour;
    protected TargetStatus mPreviousTargetStatus = TargetStatus.NotObserved;

    protected virtual void Start()
    {
        mObserverBehaviour = GetComponent<ObserverBehaviour>();
        
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged += OnObserverStatusChanged;
            mObserverBehaviour.OnBehaviourDestroyed += OnObserverDestroyed;

            OnObserverStatusChanged(mObserverBehaviour, mObserverBehaviour.TargetStatus);
        }
    }

    protected virtual void OnDestroy()
    {
        if (mObserverBehaviour)
            OnObserverDestroyed(mObserverBehaviour);
    }

    void OnObserverDestroyed(ObserverBehaviour observer)
    {
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged -= OnObserverStatusChanged;
            mObserverBehaviour.OnBehaviourDestroyed -= OnObserverDestroyed;
            mObserverBehaviour = null;
        }
    }

    void OnObserverStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        HandleTargetStatusChanged(mPreviousTargetStatus.Status, targetStatus.Status);
        mPreviousTargetStatus = targetStatus;
    }

    protected virtual void HandleTargetStatusChanged(Status previousStatus, Status newStatus)
    {
        if (ShouldBeRendered(newStatus))
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    protected bool ShouldBeRendered(Status status)
    {
        if (status == Status.TRACKED)
        {
            // always render when status is TRACKED, regardless of filter
            return true;
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked && status == Status.EXTENDED_TRACKED)
        {
            // also return true if the target is extended tracked
            return true;
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked_Limited &&
            (status == Status.EXTENDED_TRACKED || status == Status.LIMITED))
        {
            // in this mode, render even if the target's tracking status is LIMITED
            return true;
        }

        return false;
    }

    protected virtual void OnTrackingFound()
    {
        SetComponentsEnabled(true);
        OnTargetFound?.Invoke();
    }

    protected virtual void OnTrackingLost()
    {
        SetComponentsEnabled(false);
        OnTargetLost?.Invoke();
    }

    void SetComponentsEnabled(bool enable)
    {
        var components = GetComponentsInChildren<Component>();
        foreach (var component in components)
        {
            switch (component)
            {
                case Renderer rendererComponent:
                    rendererComponent.enabled = enable;
                    break;
                case Collider colliderComponent:
                    colliderComponent.enabled = enable;
                    break;
                case Canvas canvasComponent:
                    canvasComponent.enabled = enable;
                    break;
                case RuntimeMeshRenderingBehaviour runtimeMeshComponent:
                    runtimeMeshComponent.enabled = enable;
                    break;
            }
        }
    }
}