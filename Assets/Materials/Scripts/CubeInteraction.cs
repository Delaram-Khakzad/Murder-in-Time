using UnityEngine;
using System.Collections;

public class CubeInteraction : MonoBehaviour
{
    public AudioSource cubeAudioSource; // Audio source specific to this cube
    public Color redColor = Color.red; // Color for "redmaze" cubes
    public Color greenColor = Color.green; // Color for "greenmaze" cubes
    public Color clear = new(0, 0, 0, 0); // Fully transparent white color
    public Color resetColor = Color.clear;
    public float resetDelay = 3.0f; // Delay before resetting color

    private bool isResetting = false; // Flag to prevent re-triggering during reset

    private void Start()
    {
        if (cubeAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned to " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only proceed if the collider is tagged as "Player"
        if (other.CompareTag("Player") && !isResetting)
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        // Check the tag of this cube and apply color and audio accordingly
        if (CompareTag("redmaze"))
        {
            ChangeCubeColor(redColor);

            // Play audio for "redmaze" if assigned and not already playing
            if (cubeAudioSource != null && !cubeAudioSource.isPlaying)
            {
                cubeAudioSource.Play();
            }

            // Start reset coroutine for color reset
            StartCoroutine(ResetCubeColorWithDelay());
        }
        else if (CompareTag("greenmaze"))
        {
            ChangeCubeColor(greenColor);
        }
    }

    // Change the color of this cube
    private void ChangeCubeColor(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
            Debug.Log("Color changed for " + gameObject.name + " to " + color);
        }
    }

    // Coroutine to reset cube color after a delay
    private IEnumerator ResetCubeColorWithDelay()
    {
        isResetting = true;
        yield return new WaitForSeconds(resetDelay);
        ChangeCubeColor(resetColor);
        isResetting = false;
    }
}