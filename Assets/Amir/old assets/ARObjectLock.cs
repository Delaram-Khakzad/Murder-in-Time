using UnityEngine;

public class ARObjectLock : MonoBehaviour
{
    // The fixed reference position in world space
    private Vector3 referencePosition = new Vector3(0f, 1.65f, 1.06f);
    
    void Start()
    {
        // Set the object's position at the start of the game
        LockPosition();
    }

    void Update()
    {
        // Continuously lock the position to prevent it from being influenced by anything else
        LockPosition();
    }

    void LockPosition()
    {
        // Lock the object's position to the reference point
        transform.position = referencePosition;

        // Optionally, reset the rotation if needed
        transform.rotation = Quaternion.identity;  // Keeps rotation fixed
    }
}
