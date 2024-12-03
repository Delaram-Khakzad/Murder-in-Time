using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeInteraction : MonoBehaviour
{
    public AudioSource redAudioSource; // Audio source specific to "redmaze" cubes
    public AudioSource greenAudioSource; // Audio source specific to "greenmaze" cubes
    public GreenMazeManager greenMazeManager; // Reference to the GreenMazeManager
    public Color redColor = Color.red; // Color for "redmaze" cubes
    public Color greenColor = Color.green; // Color for "greenmaze" cubes
    public Color clear = new Color(0, 0, 0, 0); // Fully transparent color
    public Color flashColor = Color.red; // Flashing color (red)
    public float flashDuration = 5.0f; // Duration for flashing effect
    public float flashInterval = 0.5f; // Interval for flash effect

    private bool isFlashing = false; // Flag to indicate flashing is in progress
    public bool hasCollided = false; // Flag to prevent multiple counts for one collision

    // Static variable to track global flashing state
    private static bool isGlobalFlashingActive = false;

    private void Start()
    {
        // Validate references
        if (redAudioSource == null && CompareTag("redmaze"))
        {
            Debug.LogWarning("No AudioSource assigned for red cubes on " + gameObject.name);
        }

        if (greenAudioSource == null && CompareTag("greenmaze"))
        {
            Debug.LogWarning("No AudioSource assigned for green cubes on " + gameObject.name);
        }

        if (greenMazeManager == null)
        {
            Debug.LogError("GreenMazeManager is not assigned in " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevent any collisions during global flashing
        if (isGlobalFlashingActive)
        {
            Debug.Log("Collision blocked during global flashing");
            return;
        }

        // Only proceed if it's the player and conditions are met
        if (other.CompareTag("Player") && 
            !isFlashing && 
            !hasCollided && 
            !greenMazeManager.victoryAchieved)
        {
            HandleCollision();
        }
    }

    private void HandleCollision()
    {
        // Additional check to prevent collision after victory
        if (greenMazeManager.victoryAchieved)
        {
            Debug.Log("Victory already achieved. Ignoring collision.");
            return;
        }

        if (CompareTag("redmaze"))
        {
            // Only allow flashing if victory has not been achieved
            if (!greenMazeManager.victoryAchieved)
            {
                ChangeCubeColor(redColor);

                if (redAudioSource != null && !redAudioSource.isPlaying)
                {
                    redAudioSource.Play();
                }

                StartCoroutine(FlashAndResetAllCubes());
            }
        }
        else if (CompareTag("greenmaze"))
        {
            ChangeCubeColor(greenColor);
            hasCollided = true;

            if (greenAudioSource != null && !greenAudioSource.isPlaying)
            {
                greenAudioSource.Play();
            }

            if (greenMazeManager != null)
            {
                greenMazeManager.IncrementGreenCounter();
            }
        }
    }

    private void ChangeCubeColor(Color color)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
            Debug.Log("Color changed for " + gameObject.name + " to " + color);
        }
    }

    private IEnumerator FlashAndResetAllCubes()
    {
        // Prevent flashing if victory is achieved
        if (greenMazeManager.victoryAchieved)
        {
            Debug.Log("Cannot flash cubes - victory already achieved");
            yield break;
        }

        if (isFlashing) yield break; // Prevent duplicate flashing routines
        isFlashing = true;
        isGlobalFlashingActive = true; // Set global flashing state

        // Only get red maze cubes for flashing
        List<GameObject> redCubes = new List<GameObject>(GameObject.FindGameObjectsWithTag("redmaze"));

        float elapsed = 0f;
        bool flashOn = true;

        while (elapsed < flashDuration)
        {
            // Additional check to stop flashing if victory is achieved
            if (greenMazeManager.victoryAchieved)
            {
                break;
            }

            foreach (GameObject cube in redCubes)
            {
                Renderer renderer = cube.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = flashOn ? flashColor : clear;
                }
            }
            flashOn = !flashOn;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        // Reset only red maze cubes
        foreach (GameObject cube in redCubes)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = clear;
            }
        }

        // Reset green maze cubes separately
        GameObject[] greenCubes = GameObject.FindGameObjectsWithTag("greenmaze");
        foreach (GameObject cube in greenCubes)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = clear;
            }

            CubeInteraction cubeInteraction = cube.GetComponent<CubeInteraction>();
            if (cubeInteraction != null)
            {
                cubeInteraction.hasCollided = false;
            }
        }

        // Only reset if victory has not been achieved
        if (!greenMazeManager.victoryAchieved)
        {
            greenMazeManager.ResetGreenCounter();
        }
        
        isFlashing = false;
        isGlobalFlashingActive = false; // Reset global flashing state
    }
}