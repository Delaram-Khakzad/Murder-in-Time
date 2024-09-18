using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class LenseEventController : MonoBehaviour
{
    public Button activateButton;  // Button to trigger AR image detection
    public ImageTargetBehaviour imageTarget;  // The image target to be detected from the Vuforia database
    public GameObject objectToShow;  // The object that should appear when the image is detected (e.g., an envelope)
    public GameObject textToShow; // UI Text object that shows the text when the envelope is clicked

    private bool isDetectionActive = false;

    void Start()
    {
        // Initially hide the object attached to the image and the text
        objectToShow.SetActive(false);
        textToShow.SetActive(false);

        // Initially disable the ImageTargetBehaviour (no detection)
        imageTarget.enabled = false;

        // Add listener to the button click event
        activateButton.onClick.AddListener(ToggleImageDetection);
    }

    // This method toggles image detection on and off
    void ToggleImageDetection()
    {
        isDetectionActive = !isDetectionActive;

        if (isDetectionActive)
        {
            // Enable the ImageTargetBehaviour to allow image detection
            imageTarget.enabled = true;
            Debug.Log("Image detection activated.");

            // Subscribe to the target status change events
            imageTarget.OnTargetStatusChanged += OnTrackableStatusChanged;
        }
        else
        {
            // Disable the ImageTargetBehaviour to stop image detection
            imageTarget.enabled = false;
            Debug.Log("Image detection deactivated.");

            // Unsubscribe from the target status change events
            imageTarget.OnTargetStatusChanged -= OnTrackableStatusChanged;

            // Hide the object and text since detection is deactivated
            objectToShow.SetActive(false);
            textToShow.SetActive(false);
        }
    }

    // Event handler for tracking status changes
    void OnTrackableStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        var status = targetStatus.Status;

        // If the target is tracked or extended, show the object
        if (status == Status.TRACKED || status == Status.EXTENDED_TRACKED)
        {
            OnImageTargetDetected();
        }
        // If the target is lost or the tracking is limited, hide the object
        else if (status == Status.LIMITED || status == Status.NO_POSE)
        {
            OnImageTargetLost();
        }
    }

    // Called when the image target is detected
    void OnImageTargetDetected()
    {
        Debug.Log("Image target detected!");
        // Show the object attached to the image
        objectToShow.SetActive(true);
    }

    // Called when the image target is lost
    void OnImageTargetLost()
    {
        Debug.Log("Image target lost.");
        // Hide the object attached to the image
        objectToShow.SetActive(false);
        textToShow.SetActive(false); // Hide the text if the object is lost
    }

    // Update method to detect clicks on the object
    void Update()
    {
        // Detect mouse click or screen touch
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to detect if the object was clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is the envelope (objectToShow)
                if (hit.transform == objectToShow.transform)
                {
                    OnObjectClicked();
                }
            }
        }
    }

    // Called when the envelope is clicked
    void OnObjectClicked()
    {
        Debug.Log("Envelope clicked!");
        // Show the text when the envelope is clicked
        textToShow.SetActive(true);
        objectToShow.SetActive(false);
    }

    // Cleanup the event subscription when the script is destroyed
    void OnDestroy()
    {
        if (imageTarget != null)
        {
            imageTarget.OnTargetStatusChanged -= OnTrackableStatusChanged;
        }
    }
}
