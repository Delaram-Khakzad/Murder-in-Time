using UnityEngine;
using UnityEngine.UI;

public class EnvironmentToggle : MonoBehaviour
{
    public GameObject environment;   // Reference to the Environment object
    public Button toggleButton;      // The debug UI button for toggling visibility
    private bool isEnvironmentVisible = false;  // Track visibility state
    public AudioClip clickSound;  // Drag and drop the audio file here in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        toggleButton.onClick.AddListener(ToggleEnvironment);
        foreach (Transform child in environment.transform)
        {
            child.gameObject.SetActive(isEnvironmentVisible);
        }
    }

    public void PlayClickSound()
    {
        // Assign the AudioClip to the AudioSource and play it
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    // Method to toggle visibility of all child objects under Environment
    public void ToggleEnvironment()
    {
        isEnvironmentVisible = !isEnvironmentVisible;  // Toggle the state

        // Play the click sound
        PlayClickSound();

        // Loop through each child object of Environment and set its active state
        foreach (Transform child in environment.transform)
        {
            child.gameObject.SetActive(isEnvironmentVisible);
        }
    }
}