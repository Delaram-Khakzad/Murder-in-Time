using UnityEngine;
using Vuforia;
using System.Collections;

public class SimplePlayMusic : MonoBehaviour
{
    private AudioSource audioSource;
    private ObserverBehaviour observer;
    private Coroutine playCoroutine;

    private bool isTracking = false;

    // Volume settings
    private float minVolume = 0.02f; // 2%
    private float maxVolume = 1.0f;  // 100%
    private float maxAngle = 90f;    // Maximum angle for volume increase (90 degrees)

    void Start()
    {
        // Get the AudioSource from the AudioManager GameObject
        GameObject audioManager = GameObject.Find("AudioManager");
        if (audioManager)
        {
            audioSource = audioManager.GetComponent<AudioSource>();
            // Set initial volume to minVolume (2%)
            audioSource.volume = minVolume;
        }
        else
        {
            Debug.LogError("AudioManager GameObject not found in the scene.");
        }

        // Get the ObserverBehaviour component
        observer = GetComponent<ObserverBehaviour>();
        if (observer)
        {
            // Register the event handler
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        else
        {
            Debug.LogError("ObserverBehaviour component not found on this GameObject.");
        }
    }

    void Update()
    {
        if (isTracking)
        {
            // Get the rotation difference between the target and the camera
            Quaternion targetRotation = observer.transform.rotation;
            Quaternion cameraRotation = Camera.main.transform.rotation;

            // Calculate the relative rotation
            Quaternion relativeRotation = Quaternion.Inverse(cameraRotation) * targetRotation;

            // Get the angle around the X-axis (pitch)
            float angleDifference = Mathf.Abs(relativeRotation.eulerAngles.x);

            // Adjust angle to be within [0, 180]
            if (angleDifference > 180f)
            {
                angleDifference = 360f - angleDifference;
            }

            // Invert the angle difference
            angleDifference = maxAngle - angleDifference;

            // Ensure angleDifference is not negative
            angleDifference = Mathf.Max(angleDifference, 0f);

            // Clamp the angle between 0 and maxAngle
            angleDifference = Mathf.Clamp(angleDifference, 0f, maxAngle);

            // Calculate the volume proportionally based on the angle
            float volume = minVolume + ((angleDifference / maxAngle) * (maxVolume - minVolume));

            // Set the audio volume
            audioSource.volume = volume;

            // Debug log for volume and angle
            Debug.LogFormat("Angle Difference (X-axis): {0:F2} degrees, Volume: {1:P0}", angleDifference, volume);
        }
    }

    void OnDestroy()
    {
        if (observer)
        {
            // Unregister the event handler to prevent memory leaks
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        var status = targetStatus.Status;
        var statusInfo = targetStatus.StatusInfo;

        // Modified condition: Only consider the target found when status is TRACKED and statusInfo is NORMAL
        if (status == Status.TRACKED && statusInfo == StatusInfo.NORMAL)
        {
            OnTargetFound();
        }
        else
        {
            OnTargetLost();
        }
    }

    private void OnTargetFound()
    {
        Debug.Log("OnTargetFound() called");

        if (audioSource && !audioSource.isPlaying)
        {
            Debug.Log("Starting audio playback for 10 seconds");
            // Start playing the audio from the beginning
            audioSource.time = 0f;
            audioSource.Play();

            // Start coroutine to stop audio after 10 seconds
            if (playCoroutine != null)
            {
                StopCoroutine(playCoroutine);
            }
            playCoroutine = StartCoroutine(StopAudioAfterTime(10f));

            // Set tracking flag
            isTracking = true;
        }
    }

    private void OnTargetLost()
    {
        Debug.Log("OnTargetLost() called");

        if (audioSource && audioSource.isPlaying)
        {
            Debug.Log("Stopping audio playback due to target lost");
            // Stop the audio and reset playback position
            audioSource.Stop();

            // Reset volume to minVolume (2%) for next time
            audioSource.volume = minVolume;
        }

        // Stop the coroutine if it's running
        if (playCoroutine != null)
        {
            StopCoroutine(playCoroutine);
            playCoroutine = null;
        }

        // Reset tracking flag
        isTracking = false;
    }

    private IEnumerator StopAudioAfterTime(float time)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(time);

        // Check if the audio is still playing
        if (audioSource && audioSource.isPlaying)
        {
            Debug.Log("Stopping audio playback after 10 seconds");
            audioSource.Stop();

            // Reset volume to minVolume (2%) for next time
            audioSource.volume = minVolume;
        }

        playCoroutine = null;
    }
}
