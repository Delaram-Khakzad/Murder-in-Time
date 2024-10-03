using UnityEngine;

public class AnchorAtStart : MonoBehaviour
{
    public GameObject cubePrefab;  // The object you want to anchor
    private bool isAnchored = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Set the object to be visible at the start of the app
        cubePrefab.SetActive(true);

        // Position the cube relative to the AR camera (e.g., 1 meter in front of it)
        initialPosition = Camera.main.transform.position + Camera.main.transform.forward * 10.0f;  // 1 meter in front
        initialRotation = Quaternion.identity;  // No initial rotation

        // Place the cube at this position
        cubePrefab.transform.position = initialPosition;
        cubePrefab.transform.rotation = initialRotation;

        // Now it's anchored
        isAnchored = true;
    }

    void Update()
    {
        if (isAnchored)
        {
            // Keep the cube in the initial anchored position
            cubePrefab.transform.position = initialPosition;
            cubePrefab.transform.rotation = initialRotation;
        }
    }
}
