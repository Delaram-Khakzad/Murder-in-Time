using UnityEngine;

public class PitColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player collided with: {gameObject.name}");

            // Check for main objects
            if (gameObject.CompareTag("Object3") || 
                gameObject.CompareTag("Object4") || 
                gameObject.CompareTag("Object5") || 
                gameObject.CompareTag("Object6"))
            {
                PitManager.Instance.OnObjectCollision(gameObject);  
            }
            else if (gameObject.CompareTag("EndZone"))
            {
                // Check if the external collision sound is playing
                if (PitManager.Instance != null && PitManager.Instance.externalCollisionSound != null)
                {
                    if (PitManager.Instance.externalCollisionSound.isPlaying)
                    {
                        Debug.Log("Stopping external collision sound.");
                        PitManager.Instance.externalCollisionSound.Stop();
                    }
                    else
                    {
                        Debug.Log("External collision sound was not playing.");
                    }
                }
                else
                {
                    Debug.LogWarning("PitManager instance or external collision sound is missing!");
                }

                // Play the success sound for reaching the end zone
                if (PitManager.Instance != null && PitManager.Instance.successSound != null)
                {
                    Debug.Log("Playing success sound for end zone.");
                    PitManager.Instance.successSound.Play();
                }
                else
                {
                    Debug.LogWarning("Success sound or PitManager instance is missing!");
                }

                // Notify the PitManager that the end zone has been reached
                if (PitManager.Instance != null)
                {
                    PitManager.Instance.OnReachEndZone();
                }
            }
            // Check for external objects and reset if collided, ensuring no duplicate sound plays
            else if (gameObject.CompareTag("ExternalObject"))
            {
                Debug.Log("Player collided with an external object. Checking sound status and resetting.");

                if (PitManager.Instance != null && PitManager.Instance.externalCollisionSound != null)
                {
                    if (!PitManager.Instance.externalCollisionSound.isPlaying)
                    {
                        Debug.Log("Playing external collision sound.");
                        PitManager.Instance.OnExternalObjectCollision();
                    }
                    else
                    {
                        Debug.Log("External collision sound is already playing, resetting without sound.");
                        PitManager.Instance.ResetObjects();
                    }
                }
                else
                {
                    Debug.LogWarning("PitManager instance or external collision sound is missing!");
                }
            }
        }
    }
}
