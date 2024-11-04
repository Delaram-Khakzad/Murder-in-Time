using UnityEngine;
using UnityEngine.UI; // For UI button

public class ProjectorController : MonoBehaviour
{
    public GameObject board; // Assign your board object
    public Button onOffButton; // Assign a UI button for on/off control
    public Light projectorLight; // Add a Light component for projection effect
    public GameObject projectorLightFeatures;
    private bool isPlayerInRange = false;
    private bool isProjectorOn = false;
    public AudioClip clickSound;        // Drag and drop the audio file here in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        onOffButton.gameObject.SetActive(false);
        projectorLight.enabled = false;
        projectorLightFeatures.SetActive(false);
        board.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the projector’s collider range
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            onOffButton.gameObject.SetActive(true); // Show on/off button
        }
    }

    public void PlayClickSound()
    {
        // Assign the AudioClip to the AudioSource and play it
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the on/off button when player exits the range
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            onOffButton.gameObject.SetActive(false);
        }
    }

    // Method for on/off button click
    public void ToggleProjector()
    {
        isProjectorOn = !isProjectorOn;
        PlayClickSound();

        if (isProjectorOn)
        {
            board.SetActive(true);
            projectorLight.enabled = true; // Turn on projector light
            projectorLightFeatures.SetActive(true);
        }
        else
        {
            board.SetActive(false);
            projectorLight.enabled = false; // Turn off projector light
            projectorLightFeatures.SetActive(false);
        }
    }
}