using UnityEngine;

public class ChildTrigger : MonoBehaviour
{
    private DisappearOnChildTrigger parentScript;

    private void Start()
    {
        // Get the parent script
        parentScript = GetComponentInParent<DisappearOnChildTrigger>();
        if (parentScript == null)
        {
            Debug.LogError("Parent script not found. Ensure the parent has the DisappearOnChildTrigger script.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Adjust "Player" tag as needed
        {
            // Notify the parent script to disappear
            parentScript.DisappearParent();
        }
    }
}
