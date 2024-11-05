using System.Collections;
using UnityEngine;
using TMPro;

public class MazeCustomHandler : MonoBehaviour
{
    public GameObject objectToShow;       // The 3D object shown for 5 seconds
    public GameObject model3D;            // The 3D model shown for 60 seconds
    public TextMeshProUGUI timerText;     // UI text for countdown
    public float countdownTime = 60f;     // Countdown starting at 60 seconds
    private bool isTracking = false;      // Is tracking active
    private bool modelVisible = false;    // Is the 3D model visible
    public AudioSource mazeWarningSound;  // Warning sound for maze collisions
    public AudioSource collisionSound;    // Sound when the specific object collides
    public GameObject player;             // The player navigating the maze

    void Start() {
        // Initially hide the 3D objects and timer
        objectToShow.SetActive(false);
        model3D.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    // Called when the target is found
    public void OnTrackingFound() {
        if (!isTracking) {
            isTracking = true;
            StartCoroutine(HandleARSequence());
        }
    }

    // Called when the target is lost
    public void OnTrackingLost() {
        isTracking = false;
        objectToShow.SetActive(false);
        model3D.SetActive(false);
        timerText.gameObject.SetActive(false);
    }

    // Manages the AR sequence
    IEnumerator HandleARSequence() {
        // Show the object for 5 seconds
        objectToShow.SetActive(true);
        yield return new WaitForSeconds(5f);
        objectToShow.SetActive(false);

        // Start the countdown timer
        timerText.gameObject.SetActive(true);
        StartCoroutine(Countdown());

        // Show the 3D model for 60 seconds
        model3D.SetActive(true);
        modelVisible = true;
        yield return new WaitForSeconds(60f);
        model3D.SetActive(false);
        modelVisible = false;
    }

    // Countdown logic
    IEnumerator Countdown() {
        while (countdownTime > 0 && modelVisible) {
            timerText.text = countdownTime.ToString("F0");
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        timerText.text = "0";
        timerText.gameObject.SetActive(false);
    }

    // Deduct 5 seconds when specific object collides
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("SpecificObject")) {
            countdownTime = Mathf.Max(0, countdownTime - 5);
            collisionSound.Play();  // Play a sound when the object collides
        }
        else if (other.CompareTag("MazeWall")) {
            mazeWarningSound.Play();
            // Optionally, move the player back to the start of the maze or issue a warning
            ResetPlayerToStart();
        }
    }

    // This method resets the player to the start position of the maze
    void ResetPlayerToStart() {
        // You can modify this logic to suit your maze navigation. 
        // This assumes the player's starting position is known.
        player.transform.position = new Vector3(0f, 0f, 0f);  // Example reset position
    }
}
