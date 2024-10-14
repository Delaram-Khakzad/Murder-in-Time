using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    public GameObject[] objectsToAnchor; // Objects to anchor in their initial world position and rotation

    private Vector3[] initialPositions; // To store initial world positions of objects
    private Quaternion[] initialRotations; // To store initial world rotations of objects

    void Start()
    {
        // Initialize arrays to store the initial positions and rotations
        initialPositions = new Vector3[objectsToAnchor.Length];
        initialRotations = new Quaternion[objectsToAnchor.Length];

        // Capture the initial world position and rotation for each object
        for (int i = 0; i < objectsToAnchor.Length; i++)
        {
            if (objectsToAnchor[i] != null)
            {
                initialPositions[i] = objectsToAnchor[i].transform.position; // Store world position
                initialRotations[i] = objectsToAnchor[i].transform.rotation; // Store world rotation
            }
        }
    }

    void LateUpdate()
    {
        // Lock positions and rotations after all other updates
        LockObjectsInPlace();
    }

    private void LockObjectsInPlace()
    {
        for (int i = 0; i < objectsToAnchor.Length; i++)
        {
            if (objectsToAnchor[i] != null)
            {
                // Reset the object's position and rotation to the initial values every frame
                objectsToAnchor[i].transform.position = initialPositions[i];
                objectsToAnchor[i].transform.rotation = initialRotations[i];
            }
        }
    }
}
