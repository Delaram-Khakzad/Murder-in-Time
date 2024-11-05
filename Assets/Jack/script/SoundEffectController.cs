using System;
using UnityEngine;
using UnityEngine.UI;

public class SoundEffectController : MonoBehaviour
{
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip tapSound;       // The sound effect to play when the user taps the screen or clicks a button
    public Button yourButton;        // Reference to a UI button, if applicable
    private Boolean ButtonOrNot=false;
    void Start()
    {
        // Make sure an AudioSource component is attached and a sound is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not assigned.");
        }

        // Ensure a button is assigned if you're triggering sound on a button click
        if (yourButton != null)
        {
            ButtonOrNot = true;
            yourButton.onClick.AddListener(PlaySound);
        }
    }

    void Update()
    {
        // Detect user tap or mouse click on the screen
        if (Input.GetMouseButtonDown(0)&&ButtonOrNot==false)
        {
            PlaySound();  // Play the sound effect when the screen is tapped
        }
    }

    // Method to play the sound
    public void PlaySound()
    {
        Debug.Log(tapSound);
        if (audioSource != null && tapSound != null)
        {
            audioSource.PlayOneShot(tapSound);  // Play the assigned tap sound
        }
    }
}