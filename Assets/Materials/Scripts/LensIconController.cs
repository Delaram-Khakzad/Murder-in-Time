using UnityEngine;
using UnityEngine.UI;
using Vuforia;
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
        // Initialize the lens icon and observer behavior
        lensIcon.fillAmount = 0f; // Start with the icon unfilled
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour != null)
        {
            // Subscribe to the event when target status changes
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        else
        {
            Debug.LogError("ObserverBehaviour component not found on GameObject!");
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
        Debug.Log("OnTargetStatusChanged called: " + targetStatus.Status);

        if (targetStatus.Status == Status.TRACKED)
        {
            Debug.Log("Target is tracked, starting to fill.");
            isFilling = true;
        }
        else if (targetStatus.Status == Status.NO_POSE)
        {
            Debug.Log("Target lost, stopping fill.");
            isFilling = false;
            fillAmount = 0f; // Reset the fill amount
            lensIcon.fillAmount = fillAmount;
            lensIcon.sprite = blackWhiteLensIcon; // Switch to black-and-white icon
            lensIconBorder.SetActive(true); // Show border when not filled
        }
    }
}