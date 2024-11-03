using UnityEngine;
using System.Collections;

public class PlaySongOnCollisionupdated : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource externalObjectAudioSource; // Audio source for the external object's appearance
    public Color redColor = Color.red; // Color for redmaze objects
    public Color greenColor = Color.green; // Color for greenmaze objects
    public Color resetColor = Color.white; // Reset color for all objects
    public float resetDelay = 3.0f; // Delay in seconds before resetting colors
    public GameObject externalObject; // The object to appear after seven items turn green
    public float flashInterval = 0.5f; // Interval for flashing

    private int greenCount = 0; // Counter for green items
    private bool isResetting = false; // Flag to disable collision detection during reset

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned to " + gameObject.name);
        }

        if (externalObjectAudioSource == null)
        {
            Debug.LogWarning("No AudioSource assigned for the external object sound.");
        }

        // Ensure external object is hidden at the start
        if (externalObject != null)
        {
            externalObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isResetting) return; // Skip collision detection during reset delay

        // Check if the collided object has the "redmaze" tag
        if (other.CompareTag("redmaze"))
        {
            Debug.Log("Trigger detected with 'redmaze' tag: " + other.gameObject.name);

            // Play the audio if not already playing
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Change the object's color to red
            Renderer objectRenderer = other.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                objectRenderer.material.color = redColor;
                Debug.Log("Color changed to red for: " + other.gameObject.name);
            }

            // Start the reset coroutine with flashing effect and delay
            StartCoroutine(ResetAllMazeColorsWithDelay());
        }
        // Check if the collided object has the "greenmaze" tag
        else if (other.CompareTag("greenmaze"))
        {
            Debug.Log("Trigger detected with 'greenmaze' tag: " + other.gameObject.name);

            // Change the object's color to green without playing any sound
            Renderer objectRenderer = other.GetComponent<Renderer>();
            if (objectRenderer != null && objectRenderer.material.color != greenColor)
            {
                objectRenderer.material.color = greenColor;
                Debug.Log("Color changed to green for: " + other.gameObject.name);

                // Increase the green count
                greenCount++;

                // Check if seven items have turned green
                if (greenCount == 7)
                {
                    // Make the external object appear
                    if (externalObject != null)
                    {
                        externalObject.SetActive(true);
                        Debug.Log("Seven items turned green. External object activated.");
                    }

                    // Play the sound for the external object's appearance
                    if (externalObjectAudioSource != null)
                    {
                        externalObjectAudioSource.Play();
                        Debug.Log("External object appearance sound played.");
                    }
                }
            }
        }
    }

    // Coroutine to reset all redmaze and greenmaze objects to white after a delay with flashing effect
    private IEnumerator ResetAllMazeColorsWithDelay()
    {
        isResetting = true; // Disable collision detection

        float elapsedTime = 0f;

        // Flash objects red for the duration of the reset delay
        while (elapsedTime < resetDelay)
        {
            SetMazeObjectsColor(redColor);
            yield return new WaitForSeconds(flashInterval);
            SetMazeObjectsColor(resetColor);
            yield return new WaitForSeconds(flashInterval);

            elapsedTime += flashInterval * 2;
        }

        // Reset all redmaze and greenmaze objects to white and reset the green count
        greenCount = 0;
        SetMazeObjectsColor(resetColor);

        isResetting = false; // Re-enable collision detection
        Debug.Log("All redmaze and greenmaze objects reset to white after delay.");
    }

    // Helper method to set all maze objects' color
    private void SetMazeObjectsColor(Color color)
    {
        GameObject[] redMazeObjects = GameObject.FindGameObjectsWithTag("redmaze");
        foreach (GameObject obj in redMazeObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }

        GameObject[] greenMazeObjects = GameObject.FindGameObjectsWithTag("greenmaze");
        foreach (GameObject obj in greenMazeObjects)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
        }
    }
}
