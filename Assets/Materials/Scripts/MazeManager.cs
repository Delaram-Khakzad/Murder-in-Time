using UnityEngine;

public class GreenMazeManager : MonoBehaviour
{
    public AudioSource victoryAudioSource; // Audio source for victory sound
    public GameObject externalObject; // Object to show upon victory
    public int greenGoal = 7; // Number of green objects needed for victory

    private int greenCounter = 0; // Counter for green objects turned
    public bool victoryAchieved = false; // Flag to disable further collisions after victory

    private void Start()
    {
        // Ensure the external object is hidden at the start
        if (externalObject != null)
        {
            externalObject.SetActive(false);
        }
    }

    // Method to increment green counter and check for victory
    public void IncrementGreenCounter()
    {
        if (!victoryAchieved) // Only increment if victory hasn't been achieved
        {
            greenCounter++;
            if (greenCounter >= greenGoal)
            {
                TriggerVictory();
            }
        }
    }

    // Method to reset green counter if needed
    public void ResetGreenCounter()
    {
        greenCounter = 0;
    }

    // Trigger victory sequence: play sound and show external object
    private void TriggerVictory()
    {
        victoryAchieved = true; // Set victory flag to prevent further collisions
        if (victoryAudioSource != null && !victoryAudioSource.isPlaying)
        {
            victoryAudioSource.Play();
        }

        if (externalObject != null)
        {
            externalObject.SetActive(true);
        }

        Debug.Log("Victory achieved! Seven green objects collided.");
    }
}