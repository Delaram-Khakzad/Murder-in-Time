using UnityEngine;
using System.Collections;

public class HourglassInteraction : MonoBehaviour
{
    public AudioSource[] audioSources; // Assign multiple AudioSources in the inspector
    public float[] audioVolumes; // Array for custom volumes for each AudioSource
    public GameObject canvasPanel; // Assign the panel to display
    public float vibrationDuration = 0.1f; // Duration of the vibration
    public GameObject glassHolder; // Assign the "glass_holder" child in the inspector
    public int spinCount = 3; // Number of full rotations
    public float spinDuration = 1f; // Duration of the spin animation in seconds

    public Vector3 targetPositionOffset = new Vector3(0, 1, 0); // How high the hourglass will rise
    public Vector3 targetScale = new Vector3(1.5f, 1.5f, 1.5f); // Final scale of the hourglass

    private Vector3 initialPosition;
    private Vector3 initialScale;
    private Quaternion initialRotation;

    private void Start()
    {
        // Ensure the canvas panel is hidden initially
        if (canvasPanel != null)
        {
            canvasPanel.SetActive(false);
        }

        // Set custom volume levels for each audio source
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (i < audioVolumes.Length)
            {
                audioSources[i].volume = audioVolumes[i];
            }
            else
            {
                Debug.LogWarning("Volume array is shorter than audio sources array. Some sources may use default volume.");
            }
        }

        // Record the initial position, scale, and rotation of the hourglass
        initialPosition = transform.position;
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        ActivateHourglass();
    }

    public void ActivateHourglass()
    {
        // Play all audio sources at their specified volumes
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.Play();
            }
        }

        // Start the spin, rise, and scale animation coroutine
        StartCoroutine(AnimateHourglass());

        // Vibrate the phone for a short duration
        Vibrate();
    }

    private IEnumerator AnimateHourglass()
    {
        float totalAngle = 360f * spinCount; // Total rotation angle for the glass_holder
        float elapsed = 0f;

        Vector3 targetPosition = initialPosition + targetPositionOffset; // Final position
        Vector3 targetScaleValue = targetScale; // Final scale

        // Perform the animation over the duration
        while (elapsed < spinDuration)
        {
            float progress = elapsed / spinDuration;

            // Spin the glass holder
            float angle = Mathf.Lerp(0, totalAngle, progress);
            glassHolder.transform.localRotation = Quaternion.Euler(0, angle, 0);

            // Rise and scale the entire hourglass object
            transform.position = Vector3.Lerp(initialPosition, targetPosition, progress);
            transform.localScale = Vector3.Lerp(initialScale, targetScaleValue, progress);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final rotation, position, and scale are set precisely
        glassHolder.transform.localRotation = Quaternion.Euler(0, totalAngle, 0);

        // Reset the hourglass to its original position, scale, and rotation
        transform.position = initialPosition;
        transform.localScale = initialScale;
        transform.rotation = initialRotation;

        canvasPanel.SetActive(true);

        // // Display the canvas panel after the reset
        // if (canvasPanel != null)
        // {
        //     canvasPanel.SetActive(true);
        // }
    }

    private void Vibrate()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            Handheld.Vibrate(); // Use Handheld.Vibrate for Android
        #elif UNITY_IOS && !UNITY_EDITOR
            Handheld.Vibrate(); // Use Handheld.Vibrate for iOS (requires vibration permissions)
        #endif
    }
}