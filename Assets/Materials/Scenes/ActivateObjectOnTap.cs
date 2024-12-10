using UnityEngine;

public class ActivateObjectOnTap : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate; // Assign the object to activate in the Inspector
    private Camera arCamera;

    private void Start()
    {
        // Dynamically find the ARCamera in the scene
        arCamera = Camera.main;

        if (arCamera == null)
        {
            Debug.LogError("ARCamera not found! Ensure your ARCamera is tagged as 'MainCamera' or assign it manually.");
        }
    }

    private void Update()
    {
        if (arCamera == null) return;

        // Check for a screen tap
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Create a ray from the touch position
            Ray ray = arCamera.ScreenPointToRay(Input.GetTouch(0).position);

            // Perform a raycast
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Check if the object hit is this object
                if (hit.collider.gameObject == gameObject)
                {
                    ActivateTargetObject();
                }
            }
        }
    }

    private void ActivateTargetObject()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true); // Activate the target object
            Debug.Log("Object activated!");
        }
        else
        {
            Debug.LogWarning("Object to activate is not assigned!");
        }
    }
}
