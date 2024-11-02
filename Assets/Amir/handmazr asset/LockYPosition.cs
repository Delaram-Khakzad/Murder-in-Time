using UnityEngine;
using Vuforia;

public class BallGravityController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Disables gravity
    }

    public void OnTrackingFound()
    {
        rb.isKinematic = false; // Enables gravity when target is tracked
    }

    public void OnTrackingLost()
    {
        rb.isKinematic = true; // Disables gravity if tracking is lost
    }
}
