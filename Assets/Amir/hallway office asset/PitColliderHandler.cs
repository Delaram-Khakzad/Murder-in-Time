using UnityEngine;

public class PitColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("PitCollider"))
        {
            Debug.Log("Player hit the pit! Restarting...");
            PitManager.Instance.ResetPit(); // Reset the pit if the player touches it
        }
        else if (gameObject.CompareTag("EndZone"))
        {
            Debug.Log("Player successfully passed the pit!");
            PitManager.Instance.OnPitSuccessfullyPassed(); // Mark as success
        }
    }
}
