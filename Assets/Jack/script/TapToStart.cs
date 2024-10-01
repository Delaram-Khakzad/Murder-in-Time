using UnityEngine;
using TMPro;
using System.Collections;
public class TapToStart : MonoBehaviour
{
    public TextMeshProUGUI[] textsToFadeIn; // Array to hold the texts that will fade in
    public TextMeshProUGUI tapToStartText; // The "Tap to continue" text
    public float fadeDuration = 0.6f; // Duration for each text to fade in
    public float delayBetweenTexts = 0.2f; // Delay between fading in texts
    public float breathing_speed = 0.6f;
    public GameObject ToDeactivate; // Assign the panel you want to deactivate
    public GameObject[] ToActivate; // Assign the panel you want to activate

    // Audio components
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip fadeInSound;    // Sound effect to play when fading in starts
    public AudioSource tapToContinueSound;  // Reference to the AudioSource component
    public AudioClip tapToContinueClip;    // Sound effect to play when fading in starts
    void Start()
    {
        // Initially set the texts to transparent
        foreach (var text in textsToFadeIn)
        {
            SetTextAlpha(text, 0);
        }

        SetTextAlpha(tapToStartText, 0); // Make "Tap to continue" invisible
        BeginTextIntro();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detects a tap or mouse click
        {
            // Set ToDeactivate inactive
            audioSource.Stop();
            tapToContinueSound.PlayOneShot(tapToContinueClip);
            if (ToDeactivate != null)
            {
                ToDeactivate.SetActive(false);
            }

            // Set ToActivate active
            if (ToActivate != null)
            {
                for (int i = 0; i < ToActivate.Length; i++) { ToActivate[i].SetActive(true); }
            }
        }
    }

    public void BeginTextIntro() // Call this function after tap
    {
        StartCoroutine(FadeInTexts());
    }

    IEnumerator FadeInTexts()
    {
        // Play sound when FadeInText begins
        if (audioSource != null && fadeInSound != null)
        {
            audioSource.clip = fadeInSound;
            audioSource.loop = true;  // Set to loop in case the fade takes time
            audioSource.Play();  // Start playing the sound
        }

        // Fade in each text one by one
        foreach (var text in textsToFadeIn)
        {
            yield return StartCoroutine(FadeInText(text));
            yield return new WaitForSeconds(delayBetweenTexts);
        }

        // Stop the sound after all texts have faded in
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // After all texts fade in, show the "Tap to continue" text with a breathing effect
        StartCoroutine(FadeInText(tapToStartText));
        StartBreathingEffect();
    }

    IEnumerator FadeInText(TextMeshProUGUI text)
    {
        Color originalColor = text.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetTextAlpha(text, alpha);
            yield return null;
        }

        SetTextAlpha(text, 1); // Ensure the final alpha is fully visible
    }

    void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }

    // Start breathing effect for "Tap to continue" text
    void StartBreathingEffect()
    {
        StartCoroutine(BreathingEffect(tapToStartText));
    }

    IEnumerator BreathingEffect(TextMeshProUGUI text)
    {
        float speed = breathing_speed;
        while (true) // Loop infinitely for the breathing effect
        {
            // Fade from 0 to 1 (transparent to fully visible)
            float elapsedTime = 0f;
            while (elapsedTime < 1.0f)
            {
                elapsedTime += Time.deltaTime * speed;
                float alpha = Mathf.Clamp01(elapsedTime); // Ensure alpha stays between 0 and 1
                SetTextAlpha(text, alpha);
                yield return null;
            }

            // Fade from 1 to 0 (fully visible to transparent)
            elapsedTime = 0f;
            while (elapsedTime < 1.0f)
            {
                elapsedTime += Time.deltaTime * speed;
                float alpha = Mathf.Clamp01(1.0f - elapsedTime); // Reversing the alpha
                SetTextAlpha(text, alpha);
                yield return null;
            }
        }
    }
}
