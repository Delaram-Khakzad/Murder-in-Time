using UnityEngine;

public class TapToContinue : MonoBehaviour
{
    public GameObject ToDeactivate; // Assign the panel you want to deactivate
    public GameObject ToActivate;   // Assign the panel you want to activate
    public AudioSource audioSource; // Reference to the AudioSource component
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detects a tap or mouse click
        {
            StartGame();
        }
    }

    void StartGame()
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
