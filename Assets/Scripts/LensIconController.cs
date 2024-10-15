using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LensIconController : MonoBehaviour
{
    public UnityEngine.UI.Image lensIcon;
    public GameObject lensIconBorder;
    public Sprite coloredLensIcon;     // Colored lens icon (when activated)
    public Sprite blackWhiteLensIcon;  // Black-and-white lens icon
    public float fillSpeed = 0.5f;     // Speed at which the icon fills up
    private ObserverBehaviour observerBehaviour;  // ObserverBehaviour for tracking
    private bool isFilling = false;
    private float fillAmount = 0f;

    void Start()
    {
        // Ensure the button and icon are in the initial state
        lensIcon.fillAmount = 0f; // Start with the icon unfilled

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
            lensIcon.fillAmount = Mathf.Clamp01(fillAmount);

            if (fillAmount >= 1f)
            {
                // Icon is fully filled, stop filling and activate the button
                isFilling = false;
                lensIcon.sprite = coloredLensIcon; // Switch to colored icon
                lensIconBorder.SetActive(false);
                SceneManager.LoadScene(1);
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
}