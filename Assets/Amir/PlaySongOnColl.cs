using UnityEngine;

public class PlaySongOnCollision : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected");

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
