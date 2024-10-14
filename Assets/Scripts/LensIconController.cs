using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class LensIconController : MonoBehaviour
{
    public UnityEngine.UI.Image lensIcon;             // Black-and-white lens icon in the UI
    public Sprite coloredLensIcon;     // Colored lens icon (when activated)
    public Sprite blackWhiteLensIcon;  // Black-and-white lens icon
    public float fillSpeed = 0.5f;     // Speed at which the icon fills up
    public Button lensButton;          // The button to activate/deactivate the lens
    private bool isLensActive = false; // Track if the lens is active or not
    private ObserverBehaviour observerBehaviour;  // ObserverBehaviour for tracking
    private bool isFilling = false;
    private float fillAmount = 0f;

    void Start()
    {
        // Ensure the button and icon are in the initial state
        lensIcon.fillAmount = 0f; // Start with the icon unfilled
        lensButton.gameObject.SetActive(false); // Deactivate button until fully filled

        // Get the ObserverBehaviour (formerly TrackableBehaviour)
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour != null)
        {
            // Subscribe to the event when target status changes
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    void Update()
    {
        if (isFilling)
        {
            // Gradually fill the icon
            fillAmount += fillSpeed * Time.deltaTime;
            lensIcon.fillAmount = fillAmount;

            if (fillAmount >= 1f)
            {
                // Icon is fully filled, stop filling and activate the button
                isFilling = false;
                lensIcon.sprite = coloredLensIcon; // Switch to colored icon
                lensButton.gameObject.SetActive(true); // Activate the button
            }
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED)
        {
            // Start filling when the real-world lens image is tracked
            isFilling = true;
        }
        else if (targetStatus.Status == Status.NO_POSE)
        {
            // Stop filling when the image is lost
            isFilling = false;
        }
    }

    public void ToggleLens()
    {
        // This function will be called when the button is clicked
        isLensActive = !isLensActive;

        if (isLensActive)
        {
            // Activate the lens, set icon to colored
            lensIcon.sprite = coloredLensIcon;
            // Additional functionality to activate lens behavior can go here
        }
        else
        {
            // Deactivate the lens, set icon to black-and-white
            lensIcon.sprite = blackWhiteLensIcon;
            // Additional functionality to deactivate lens behavior can go here
        }
    }
}