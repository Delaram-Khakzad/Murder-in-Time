using UnityEngine;
using System.Collections;

public class CubeInteraction : MonoBehaviour
{
    public AudioSource cubeAudioSource; // Audio source specific to this cube
    public GreenMazeManager greenMazeManager; // Reference to the GreenMazeManager
    public Color redColor = Color.red; // Color for "redmaze" cubes
    public Color greenColor = Color.green; // Color for "greenmaze" cubes
    public Color clear = new Color(0, 0, 0, 0); // Fully transparent color
    public Color flashColor = Color.red; // Flashing color (red)
    public float resetDelay = 3.0f; // Delay before resetting color
    public float flashDuration = 5.0f; // Duration for flashing effect
    public float flashInterval = 0.5f; // Interval for flash effect

    private bool isResetting = false; // Flag to prevent re-triggering during reset
    private bool hasCollided = false; // Flag to prevent multiple counts for one collision

    private void Start()
    {
        if (cubeAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned to " + gameObject.name);
        }

        if (greenMazeManager == null)
        {
            Debug.LogError("GreenMazeManager is not assigned in " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only proceed if the collider is tagged as "Player", victory hasn't been achieved, and no reset or previous collision has happened
        if (other.CompareTag("Player") && !isResetting && !hasCollided && !greenMazeManager.victoryAchieved)
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

            // Start flashing and reset coroutine for all tagged cubes
            StartCoroutine(FlashAndResetAllCubes());
        }
        else if (CompareTag("greenmaze"))
        {
            // Change color to green, mark as collided, and notify the manager
            ChangeCubeColor(greenColor);
            hasCollided = true; // Prevent further collision counts

            if (greenMazeManager != null)
            {
                greenMazeManager.IncrementGreenCounter();
            }
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

    // Coroutine to flash all cubes and reset color after a delay
    private IEnumerator FlashAndResetAllCubes()
    {
        isResetting = true;
        GameObject[] allCubes = GameObject.FindGameObjectsWithTag("redmaze");
        GameObject[] greenCubes = GameObject.FindGameObjectsWithTag("greenmaze");

        foreach (GameObject cube in greenCubes)
        {
            allCubes = Append(allCubes, cube);
        }

        // Flash all cubes with red color for the specified duration
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
            elapsed += flashInterval;
            yield return new WaitForSeconds(flashInterval);
        }

        // Reset all cubes to their original clear color after flashing
        foreach (GameObject cube in allCubes)
        {
            Renderer renderer = cube.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = clear;
            }

            // Reset hasCollided flag to allow re-collisions after reset, unless victory is achieved
            CubeInteraction cubeInteraction = cube.GetComponent<CubeInteraction>();
            if (cubeInteraction != null && !greenMazeManager.victoryAchieved)
            {
                cubeInteraction.hasCollided = false;
            }
        }

        // Reset green counter if victory hasn't been achieved
        if (!greenMazeManager.victoryAchieved)
        {
            greenMazeManager.ResetGreenCounter();
        }

        isResetting = false;
    }

    // Helper function to append game objects to an array
    private GameObject[] Append(GameObject[] array, GameObject item)
    {
        GameObject[] result = new GameObject[array.Length + 1];
        array.CopyTo(result, 0);
        result[result.Length - 1] = item;
        return result;
    }
}
