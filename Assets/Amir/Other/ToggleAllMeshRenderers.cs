using UnityEngine;

public class ToggleAllMeshRenderers : MonoBehaviour
{
    private MeshRenderer[] meshRenderers;

    private void Awake()
    {
        // Get all MeshRenderers in this object and its children
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        if (meshRenderers.Length > 0)
        {
            // Disable all MeshRenderers initially
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = false;
            }
        }
        else
        {
            Debug.LogError("No MeshRenderers found on this object or its children.");
        }
    }

    // This function will be called by the button click
    public void ToggleAllVisibility()
    {
        if (meshRenderers.Length > 0)
        {
            // Toggle the enabled state of all MeshRenderers
            bool newState = !meshRenderers[0].enabled; // Use the state of the first renderer as reference
            foreach (var renderer in meshRenderers)
            {
                renderer.enabled = newState;
            }

            Debug.Log("Toggled visibility of all objects to: " + newState);
        }
    }
}
