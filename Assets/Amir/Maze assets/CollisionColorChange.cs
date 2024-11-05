using UnityEngine;
using System.Collections;

public class PlaySongOnCollisionupdated : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource externalObjectAudioSource; 
    public Color redColor = Color.red; 
    public Color greenColor = Color.green; 
    public Color resetColor = Color.white;
    public float resetDelay = 3.0f; 
    public GameObject externalObject; 
    public float flashInterval = 0.5f;

    private int greenCount = 0;
    private bool isResetting = false;

    private void Start()
    {
        if (audioSource == null) Debug.LogWarning("No AudioSource assigned to " + gameObject.name);
        if (externalObjectAudioSource == null) Debug.LogWarning("No AudioSource assigned for the external object sound.");
        if (externalObject != null) externalObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only proceed if the colliding object has the Player tag
        if (isResetting || !other.CompareTag("Player")) return;

        // Detect collision with redmaze and greenmaze tags
        if (other.gameObject.CompareTag("redmaze"))
        {
            Debug.Log("Player collided with 'redmaze' tagged object: " + other.gameObject.name);

            if (audioSource != null && !audioSource.isPlaying) audioSource.Play();

            Renderer objectRenderer = other.GetComponent<Renderer>();
            if (objectRenderer != null) objectRenderer.material.color = redColor;

            StartCoroutine(ResetAllMazeColorsWithDelay());
        }
        else if (other.gameObject.CompareTag("greenmaze"))
        {
            Debug.Log("Player collided with 'greenmaze' tagged object: " + other.gameObject.name);

            Renderer objectRenderer = other.GetComponent<Renderer>();
            if (objectRenderer != null && objectRenderer.material.color != greenColor)
            {
                objectRenderer.material.color = greenColor;
                greenCount++;

                if (greenCount == 7)
                {
                    if (externalObject != null) externalObject.SetActive(true);
                    if (externalObjectAudioSource != null) externalObjectAudioSource.Play();
                }
            }
        }
    }

    private IEnumerator ResetAllMazeColorsWithDelay()
    {
        isResetting = true;
        float elapsedTime = 0f;

        while (elapsedTime < resetDelay)
        {
            SetMazeObjectsColor(redColor);
            yield return new WaitForSeconds(flashInterval);
            SetMazeObjectsColor(resetColor);
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval * 2;
        }

        greenCount = 0;
        SetMazeObjectsColor(resetColor);
        isResetting = false;
    }

    private void SetMazeObjectsColor(Color color)
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("redmaze"))
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null) renderer.material.color = color;
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("greenmaze"))
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null) renderer.material.color = color;
        }
    }
}
