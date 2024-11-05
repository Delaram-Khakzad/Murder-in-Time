using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LensToggle : MonoBehaviour

{
    public Button lensToggleButton;     // The UI button for toggling the lens
    public Image lensIcon;              // The UI image component for the lens icon
    public Sprite coloredLensIcon;      // The colored version of the lens icon
    public Sprite bwLensIcon;           // The black and white version of the lens icon
    public GameObject backgroundImage;  // The background image shown when the lens is on
    public AudioSource clickSound;
    public bool isLensActive = true;
    private readonly List<GameObject> lensControlledObjects = new();

    void Start()
    {
        lensIcon.sprite = coloredLensIcon;
        backgroundImage.SetActive(true);
        lensToggleButton.onClick.AddListener(ToggleLens);

        // Populate LensControlled objects initially
        PopulateLensControlledObjects();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene change event
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PopulateLensControlledObjects(); // Refresh lensControlledObjects on new scene load
    }

    private void PopulateLensControlledObjects()
    {
        lensControlledObjects.Clear(); // Clear the list before repopulating
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("LensControlled");
        foreach (GameObject obj in foundObjects)
        {
            lensControlledObjects.Add(obj);
        }
    }

    public void PlayClickSound()
    {
        clickSound.Play();
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