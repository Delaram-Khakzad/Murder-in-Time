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
    public float autoResetInterval = 60.0f; // Interval for automatic reset

    private bool isFlashing = false; // Flag to indicate flashing is in progress
    private bool hasCollided = false; // Flag to prevent multiple counts for one collision

    private void Start()
    {
        if (redAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned for red cubes on " + gameObject.name);
        }

        if (greenAudioSource == null)
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
    Debug.Log($"Trigger detected: {other.gameObject.name} with tag {other.tag}");
    if (other.CompareTag("Player") && !isFlashing && !hasCollided && !greenMazeManager.victoryAchieved)
    {
        HandleCollision();
    }
}


    private void HandleCollision()
    {
        if (CompareTag("redmaze"))
        {
            ChangeCubeColor(redColor);

            if (redAudioSource != null && !redAudioSource.isPlaying)
            {
                redAudioSource.Play();
            }

            StartCoroutine(FlashAndResetAllCubes());
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
        if (isFlashing) yield break; // Prevent duplicate flashing routines
        isFlashing = true;

        // Get all "redmaze" and "greenmaze" cubes
        List<GameObject> allCubes = new List<GameObject>(GameObject.FindGameObjectsWithTag("redmaze"));
        allCubes.AddRange(GameObject.FindGameObjectsWithTag("greenmaze"));

        float elapsed = 0f;
        bool flashOn = true;

        while (elapsed < flashDuration)
        {
            foreach (GameObject cube in allCubes)
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

        // Reset all cubes to their original clear color
        foreach (GameObject cube in allCubes)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = clear;
            }

            CubeInteraction cubeInteraction = cube.GetComponent<CubeInteraction>();
            if (cubeInteraction != null && cubeInteraction.CompareTag("greenmaze"))
            {
                cubeInteraction.hasCollided = false;
            }
        }

        greenMazeManager.ResetGreenCounter();
        isFlashing = false;
    }
}
