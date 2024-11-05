using UnityEngine;
using UnityEngine.Video;

public class TapToContinue : MonoBehaviour
{
    public GameObject ToDeactivate; // Assign the panel you want to deactivate
    public GameObject ToActivate;   // Assign the panel you want to activate
    public AudioSource audioSource; // Reference to the AudioSource component
    public VideoPlayer videoPlayer;  // Reference to the VideoPlayer component
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detects a tap or mouse click
        {
            Next();
            // Set ToDeactivate inactive
            if (videoPlayer != null && videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
                Debug.Log("Video stopped.");
            }
        }
    }

    void Next()
    {
        // Set ToDeactivate inactive
        if (ToDeactivate != null)
        {
            ToDeactivate.SetActive(false);
        }

        // Set ToActivate active
        if (ToActivate != null)
        {
            ToActivate.SetActive(true);
        }
        // Check if the audio source is assigned
        if (audioSource != null)
        {
            // Play the audio when the game starts
            audioSource.Play();
            Debug.Log("Intro");
        }
    }
}
