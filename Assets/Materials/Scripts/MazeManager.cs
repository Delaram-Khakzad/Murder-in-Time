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

    public void IncrementGreenCounter()
    {
        if (!victoryAchieved)
        {
            greenCounter++;
            Debug.Log($"Green Counter Incremented: {greenCounter}"); // Log green counter value
            if (greenCounter >= greenGoal)
            {
                TriggerVictory();
            }
        }
    }

    public void ResetGreenCounter()
    {
        greenCounter = 0;
        Debug.Log("Green Counter Reset");
    }

    private void TriggerVictory()
    {
        if (!victoryAchieved)
        {
            victoryAchieved = true; // Set victory flag to prevent further collisions
            Debug.Log("Triggering Victory Sequence");

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
}
