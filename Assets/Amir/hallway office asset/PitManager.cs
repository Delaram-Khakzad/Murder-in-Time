using UnityEngine;
using Vuforia;

public class PitManager : MonoBehaviour
{
    public static PitManager Instance;

    public GameObject pitQuad;  // Assign PitQuad from the Hierarchy
    public PlaneFinderBehaviour planeFinder;  // Assign Plane Finder
    public GameObject interactiveItem;  // Assign Interactive Item
    public GameObject finalObject1;  // First object on success
    public GameObject finalObject2;  // Second object on success
    public AudioSource successSound;  // Success sound

    private bool waitingForPlacement = false;  // Track if waiting for a surface hit
    private bool pitPlaced = false;  // Track if the pit is placed

    private void Awake()
    {
        Instance = this;

        // Ensure everything starts hidden
        pitQuad.SetActive(false);
        finalObject1.SetActive(false);
        finalObject2.SetActive(false);
    }

    // Activate the Plane Finder and start waiting for placement
    public void ActivatePlaneFinder()
    {
        if (pitPlaced) return;  // Prevent re-placing if already placed

        Debug.Log("Interactive item touched! Activating Plane Finder.");

        planeFinder.enabled = true;  // Enable the Plane Finder
        waitingForPlacement = true;  // Start waiting for a surface hit

        interactiveItem.SetActive(false);  // Hide the interactive item
    }

    // Place the PitQuad at the detected surface
    public void PlacePitAtSurface(HitTestResult result)
{
    if (result != null && waitingForPlacement)
    {
        Vector3 position = result.Position;
        Debug.Log($"Placing PitQuad at: {position}");

        // Set the PitQuad position and activate it
        pitQuad.transform.position = position;
        pitQuad.SetActive(true);

        // Ensure child objects are enabled
        //EnableChildObjects(pitQuad);

        waitingForPlacement = false;
        pitPlaced = true;

        planeFinder.enabled = false;  // Disable the Plane Finder
    }
}



private void EnableChildObjects(GameObject parent)
{
    foreach (Transform child in parent.transform)
    {
        child.gameObject.SetActive(true);
    }
}



    // Disable all colliders in the PitQuad to prevent collision during placement
    private void DisableColliders()
    {
        Collider[] colliders = pitQuad.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
    }

    // Re-enable colliders after a slight delay
    private void EnableColliders()
    {
        Collider[] colliders = pitQuad.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }
    }

    // Reset the pit for a new attempt
    public void ResetPit()
    {
        Debug.Log("Resetting Pit.");

        pitQuad.SetActive(false);  // Hide the pit

        interactiveItem.SetActive(true);  // Reactivate the interactive item

        pitPlaced = false;  // Reset the placement state
    }

    // Handle success when the player passes the pit
    public void OnPitSuccessfullyPassed()
    {
        finalObject1.SetActive(true);
        finalObject2.SetActive(true);

        if (successSound != null)
        {
            successSound.Play();
        }

        Debug.Log("Congratulations! You successfully passed the pit.");
    }
}
