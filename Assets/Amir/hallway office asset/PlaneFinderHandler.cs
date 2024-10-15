using UnityEngine;
using Vuforia;

public class PlaneFinderHandler : MonoBehaviour
{
    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (result != null)
        {
            Debug.Log("Surface detected. Placing PitQuad.");
            PitManager.Instance.PlacePitAtSurface(result);  // Place the PitQuad
        }
        else
        {
            Debug.LogWarning("No valid surface detected.");
        }
    }
}
