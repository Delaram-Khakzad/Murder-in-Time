using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // Required for IEnumerator

public class BoundaryWarningScript : MonoBehaviour
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

        // Vibrate the phone
        VibratePhone();
    }

    // Method to vibrate the phone
    private void VibratePhone()
    {
        #if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();  // Vibrate the phone (only works on mobile devices)
        #endif
    }
}