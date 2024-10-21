using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Interactive item touched. Activating plane finder.");
        PitManager.Instance.ActivatePlaneFinder();  // Activate the Plane Finder from PitManager
    }
}
