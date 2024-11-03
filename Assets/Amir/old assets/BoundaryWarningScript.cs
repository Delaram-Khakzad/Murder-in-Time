using UnityEngine;

public class BoundaryWarningScript : MonoBehaviour
{
    public AudioSource audioSource;
    private bool canPlaySound = false;

    private void Start()
    {
        // Activate sound only after a delay to avoid triggering at start
        Invoke("EnableSound", 1f);  // Adjust delay as necessary
    }

    private void EnableSound()
    {
        canPlaySound = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding has the specific tag (e.g., "Player")
        if (canPlaySound && other.CompareTag("Player"))
        {
            Debug.Log("Collision detected with: " + other.gameObject.name);

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
