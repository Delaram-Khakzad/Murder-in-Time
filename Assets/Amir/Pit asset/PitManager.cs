using UnityEngine;
using Vuforia;

public class PitManager : MonoBehaviour
{
    public static PitManager Instance;

    public GameObject pitQuad;  
    public PlaneFinderBehaviour planeFinder;
    public GameObject finalObject1;
    public GameObject finalObject2;
    public GameObject object3;  
    public GameObject object4;
    public GameObject object5;
    public GameObject object6;
    public GameObject externalObject1;
    public GameObject externalObject2;
    public GameObject externalObject3;
    public GameObject externalObject4;
    public AudioSource successSound;
    public AudioSource externalCollisionSound;  // Sound for external object collisions

    private int placementStage = 0;
    private bool object3Collided = false;
    private bool object4Collided = false;
    private bool object5Collided = false;
    private bool object6Collided = false;

    private void Awake()
    {
        Instance = this;
        ResetObjects();
    }

    public void ResetObjects()
    {
        // Ensure pitQuad is active from the start
        pitQuad.SetActive(true);

        // Deactivate all objects for initial setup
        object3.SetActive(false);
        object4.SetActive(false);
        object5.SetActive(false);
        object6.SetActive(false);
        finalObject1.SetActive(false);
        finalObject2.SetActive(false);

        // Ensure all external objects are active and visible from the beginning
        externalObject1.SetActive(true);
        externalObject2.SetActive(true);
        externalObject3.SetActive(true);
        externalObject4.SetActive(true);

        // Reset collision and placement states
        object3Collided = false;
        object4Collided = false;
        object5Collided = false;
        object6Collided = false;
        placementStage = 0;

        // Reset colors of all objects to default (e.g., white)
        ResetColor(object3);
        ResetColor(object4);
        ResetColor(object5);
        ResetColor(object6);

        Debug.Log("All objects reset.");
    }

    // Helper function to reset color to default
    private void ResetColor(GameObject obj)
    {
        Renderer objRenderer = obj.GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.material.color = Color.white;  // Set to the default color, white
            Debug.Log($"{obj.name} color reset to default.");
        }
    }

    // Activate Plane Finder
    public void ActivatePlaneFinder()
    {
        if (placementStage >= 4) return;

        planeFinder.enabled = true;
        Debug.Log($"Activating Plane Finder for placement stage {placementStage + 1}.");
    }

    // Handle interactive hit test
    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (result == null || placementStage >= 4) return;

        Vector3 position = result.Position;
        Debug.Log($"Placing object at stage {placementStage}.");

        switch (placementStage)
        {
            case 0:
                SetupObject(object3, position, "Object3");
                externalObject1.SetActive(false); // Deactivate externalObject1 when object3 is placed
                Debug.Log("Deactivated externalObject1.");
                break;

            case 1:
                SetupObject(object4, position, "Object4");
                externalObject2.SetActive(false); // Deactivate externalObject2 when object4 is placed
                Debug.Log("Deactivated externalObject2.");
                break;

            case 2:
                SetupObject(object5, position, "Object5");
                externalObject3.SetActive(false); // Deactivate externalObject3 when object5 is placed
                Debug.Log("Deactivated externalObject3.");
                break;

            case 3:
                SetupObject(object6, position, "Object6");
                externalObject4.SetActive(false); // Deactivate externalObject4 when object6 is placed
                Debug.Log("Deactivated externalObject4.");
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
        else if (collidedObject.CompareTag("Object5") && object4Collided && !object5Collided)
        {
            object5Collided = true;
            ChangeColorToGreen(object5);
            Debug.Log("Player collided with object5.");
        }
        else if (collidedObject.CompareTag("Object6") && object5Collided && !object6Collided)
        {
            object6Collided = true;
            ChangeColorToGreen(object6);
            Debug.Log("Player collided with object6.");
        }
    }

    // Play sound and reset on external object collision
    public void OnExternalObjectCollision()
    {
        if (externalCollisionSound != null && !externalCollisionSound.isPlaying)
        {
            externalCollisionSound.Play();
        }
        ResetObjects();
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
        if (object3Collided && object4Collided && object5Collided && object6Collided)
        {
            if (successSound != null)
            {
                successSound.Play();  
            }

            Debug.Log("Success! You reached the end zone after colliding with all objects.");
        }
        else
        {
            Debug.Log("Failed to reach the end zone correctly. Restarting.");
            ResetObjects();
        }
    }
}
