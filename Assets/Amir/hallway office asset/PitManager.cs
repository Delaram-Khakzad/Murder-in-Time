using UnityEngine;
using Vuforia;

public class PitManager : MonoBehaviour
{
    public static PitManager Instance;

    public GameObject pitQuad;  
    public PlaneFinderBehaviour planeFinder;
    public GameObject interactiveItem;
    public GameObject finalObject1;
    public GameObject finalObject2;
    public GameObject object3;  
    public GameObject object4;
    public AudioSource successSound;

    private int placementStage = 0;
    private bool object3Collided = false;
    private bool object4Collided = false;

    private void Awake()
    {
        Instance = this;
        ResetObjects();
    }

    private void ResetObjects()
    {
        pitQuad.SetActive(false);
        object3.SetActive(false);
        object4.SetActive(false);
        finalObject1.SetActive(false);
        finalObject2.SetActive(false);

        object3Collided = false;
        object4Collided = false;
        placementStage = 0;
    }

    // Activate Plane Finder
    public void ActivatePlaneFinder()
    {
        if (placementStage >= 3) return; 

        planeFinder.enabled = true;
        Debug.Log($"Activating Plane Finder for placement stage {placementStage + 1}.");
    }

    // Handle interactive hit test
    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (result == null || placementStage >= 3) return;

        Vector3 position = result.Position;
        Debug.Log($"Placing object at stage {placementStage}.");

        switch (placementStage)
        {
            case 0:
                pitQuad.transform.position = position;
                pitQuad.SetActive(true);
                Debug.Log("Placed PitQuad.");
                break;

            case 1:
                SetupObject(object3, position, "Object3");
                break;

            case 2:
                SetupObject(object4, position, "Object4");
                break;
        }

        placementStage++;
    }

    // Setup objects dynamically
    private void SetupObject(GameObject obj, Vector3 position, string tag)
    {
        obj.transform.position = position;
        obj.SetActive(true);
        obj.tag = tag;

        // Ensure the collider and script are enabled
        if (!obj.TryGetComponent(out PitColliderHandler handler))
        {
            handler = obj.AddComponent<PitColliderHandler>();
        }

        Collider collider = obj.GetComponent<Collider>();
        if (collider != null && !collider.enabled)
        {
            collider.enabled = true;
        }

        Debug.Log($"Placed {tag} and set up components.");
    }

    // Handle object collision
    public void OnObjectCollision(GameObject collidedObject)
    {
        if (collidedObject.CompareTag("Object3") && !object3Collided)
        {
            object3Collided = true;
            ChangeColorToGreen(object3);
            Debug.Log("Player collided with object3.");
        }
        else if (collidedObject.CompareTag("Object4") && object3Collided && !object4Collided)
        {
            object4Collided = true;
            ChangeColorToGreen(object4);
            Debug.Log("Player collided with object4.");
        }
    }

    // Change color to green
    private void ChangeColorToGreen(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.material.color = Color.green;
            Debug.Log($"{obj.name} turned green.");
        }
    }

    // Handle reaching the end zone
    public void OnReachEndZone()
    {
        if (object3Collided && object4Collided)
        {
            finalObject1.SetActive(true);
            finalObject2.SetActive(true);

            if (successSound != null)
            {
                successSound.Play();  
            }

            Debug.Log("Success! You reached the end zone after colliding with both objects.");
        }
        else
        {
            Debug.Log("Failed to reach the end zone correctly. Restarting.");
            ResetObjects();
        }
    }
}
