using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LensToggle : MonoBehaviour

{
    public ProjectorController projectorController;
    public Button lensToggleButton;     // The UI button for toggling the lens
    public Image lensIcon;              // The UI image component for the lens icon
    public Sprite coloredLensIcon;      // The colored version of the lens icon
    public Sprite bwLensIcon;           // The black and white version of the lens icon
    public GameObject backgroundImage;  // The background image shown when the lens is on
    public AudioClip clickSound;        // Drag and drop the audio file here in the Inspector
    private AudioSource audioSource;
    public bool isLensActive = true;
    private readonly List<GameObject> lensControlledObjects = new();

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        lensIcon.sprite = coloredLensIcon;
        backgroundImage.SetActive(true);
        lensToggleButton.onClick.AddListener(ToggleLens);

        // Store all LensControlled objects in the list
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("LensControlled");
        foreach (GameObject obj in foundObjects)
        {
            lensControlledObjects.Add(obj);
        }
    }

    public void PlayClickSound()
    {
        // Assign the AudioClip to the AudioSource and play it
        audioSource.clip = clickSound;
        audioSource.Play();
    }

    private void ToggleVisibility()
    {
        foreach (GameObject obj in lensControlledObjects)
        {
            obj.SetActive(isLensActive);
        }
    }

    public void ToggleLens()
    {
        isLensActive = !isLensActive;
        PlayClickSound();

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

        ToggleVisibility();
    }
}