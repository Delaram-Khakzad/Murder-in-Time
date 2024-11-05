using UnityEngine;

public class LockToWorldPosition : MonoBehaviour
{
    private Vector3 initialPosition;  // To store the initial world position
    private Quaternion initialRotation;  // To store the initial world rotation

    void Start()
    {
        // Capture the object's initial position and rotation when the scene starts
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Every frame, lock the object's position and rotation to the initial values
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
