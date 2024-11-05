using UnityEngine;

public class ToggleMeshRendererOnObject : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // Get the MeshRenderer component when the game starts
        meshRenderer = GetComponent<MeshRenderer>();

        // Optionally, you can start with the MeshRenderer disabled
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false; // Ensure the object is invisible at the start
        }
        else
        {
            Debug.LogError("No MeshRenderer found on this object.");
        }
    }

    // This function will be called by the button click
    public void ToggleVisibility()
    {
        if (meshRenderer != null)
        {
            // Toggle the enabled state of the MeshRenderer
            meshRenderer.enabled = !meshRenderer.enabled;
        }
    }
}
