using UnityEngine;
using UnityEngine.UI;

public class LensToggle : MonoBehaviour
{
    
    public Button lensToggleButton;     // The UI button for toggling the lens
    public Image lensIcon;              // The UI image component for the lens icon
    public Sprite coloredLensIcon;      // The colored version of the lens icon
    public Sprite bwLensIcon;           // The black and white version of the lens icon
    public GameObject backgroundImage;  // The background image shown when the lens is on
    public AudioClip clickSound;  // Drag and drop the audio file here in the Inspector
    private AudioSource audioSource;
    public bool isLensActive = true;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        lensIcon.sprite = coloredLensIcon;
        backgroundImage.SetActive(true);
        lensToggleButton.onClick.AddListener(ToggleLens);
    }

    public void PlayClickSound()
    {
        // Assign the AudioClip to the AudioSource and play it
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    public void ToggleLens()
    {
        Debug.Log("Lens Toggle Clicked!");
        // Toggle the lens state
        isLensActive = !isLensActive;

        // Play the click sound
        PlayClickSound();

        // Switch between the colored and black and white icons
        if (isLensActive)
        {
            lensIcon.sprite = coloredLensIcon;
            backgroundImage.SetActive(true);  // Show the background image
        }
        else
        {
            lensIcon.sprite = bwLensIcon;
            backgroundImage.SetActive(false);  // Hide the background image
        }
    }
}