using UnityEngine;
using Vuforia;

public class PlaneFinderHandler : MonoBehaviour
{
    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (result != null)
        {
            Debug.Log("Surface detected. Passing result to PitManager.");
            PitManager.Instance.OnInteractiveHitTest(result);  // Call the method in PitManager
        }
        else
        {
            Debug.LogWarning("No valid surface detected.");
        }
    }
}
