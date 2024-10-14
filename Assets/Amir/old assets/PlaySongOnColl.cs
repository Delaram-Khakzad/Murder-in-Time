using UnityEngine;

public class PlaySongOnCollision : MonoBehaviour
{
    public AudioSource audioSource;

    // This method is called when the trigger collides with another object
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Detected with: " + other.gameObject.name);

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
