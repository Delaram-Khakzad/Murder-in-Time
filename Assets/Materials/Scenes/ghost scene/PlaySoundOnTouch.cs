using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class PlaySoundAndChangeUIColor : MonoBehaviour
{
    public AudioClip touchSound; // Assign the sound clip in the Inspector
    public GameObject targetUIElement; // Assign the UI element in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Optional: Configure AudioSource settings
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0.0f; // UI sound is 2D
    }

    void OnTriggerEnter(Collider other)
    {
        // Play the sound
        if (touchSound != null && audioSource != null)
        {
            audioSource.clip = touchSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No audio clip assigned or AudioSource missing!");
        }

        // Change the fill amount of the target UI element
        if (targetUIElement != null)
        {
            Image uiImage = targetUIElement.GetComponent<Image>();
            if (uiImage != null)
            {
                // Check if the Image Type is "Filled"
                if (uiImage.type == Image.Type.Filled)
                {
                    // Gradually fill the image
                    StartCoroutine(FillImage(uiImage));
                }
                else
                {
                    Debug.LogWarning("Target UI element is not set to 'Filled' type!");
                }
            }
            else
            {
                Debug.LogWarning("Target UI element does not have an Image component!");
            }
        }
        else
        {
            Debug.LogWarning("Target UI element is not assigned!");
        }
    }

    private System.Collections.IEnumerator FillImage(Image image)
    {
        float duration = 1.0f; // Time to fully fill
        float startFill = image.fillAmount;
        float targetFill = 1.0f; // Fully filled
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            image.fillAmount = Mathf.Lerp(startFill, targetFill, elapsedTime / duration);
            yield return null;
        }

        image.fillAmount = targetFill;
    }
}
