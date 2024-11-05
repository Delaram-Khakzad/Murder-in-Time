using UnityEngine;
using TMPro; // For TextMeshPro
using UnityEngine.UI; // For Unity's normal UI Text

public class BreathingTextEffect : MonoBehaviour
{
    public TextMeshProUGUI TapToContinue; // Reference for TextMeshPro

    public float speed = 2.0f;   // Speed of the breathing effect

    private Color originalColor;

    void Start()
    {
        if (TapToContinue == null)
        {
            TapToContinue = GetComponent<TextMeshProUGUI>(); // Automatically get the TextMeshPro component
        }
        originalColor = TapToContinue.color; // Store the original TapToContinue color
    }

    void Update()
    {
        // Create a breathing (fade in/out) effect using Mathf.PingPong
        float alpha = Mathf.PingPong(Time.time * speed, 1.0f);
        TapToContinue.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // Change only alpha
    }
}