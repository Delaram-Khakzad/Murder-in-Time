using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Interactive item touched!");  // Confirm interaction

        // Trigger the PitManager to activate the plane finder
        PitManager.Instance.ActivatePlaneFinder();
        
        // Hide the interactive item
        gameObject.SetActive(false); 
    }
}
