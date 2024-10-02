using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using System;
using System.Reflection;

public class ActiveLenseEventController : MonoBehaviour
{
    public Button activateButton;  // Button to trigger AR image detection
    public ImageTargetBehaviour[] imageTargets;  // Array of image targets to be detected from the Vuforia database
    public GameObject[] objectsToShow;  // Array of objects that should appear when the image is detected
    public GameObject[] dialogPanel; // The dialog panel that will pop up when any object is clicked
    public Button closeButton; // The button to close the dialog panel
    public GameObject whiteScreenPanel; 
    private bool isDetectionActive = false;
    private Quaternion[] originalRotations;  // Array to store the original rotations of each object
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip crystalballRotating;    // Sound effect
    void Start()
    {
        // Initialize the originalRotations array
        originalRotations = new Quaternion[objectsToShow.Length];

        // Store the original rotations of each object
        for (int i = 0; i < objectsToShow.Length; i++)
        {
            originalRotations[i] = objectsToShow[i].transform.rotation;
        }
        // Initially hide all objects attached to the images and the dialog panel
        foreach (var obj in objectsToShow)
        {
            obj.SetActive(false);
        }
        foreach (var obj in dialogPanel)
        {
            obj.SetActive(false);
        }

        // Initially disable all ImageTargetBehaviours (no detection)
        foreach (var target in imageTargets)
        {
            target.enabled = false;
        }

        // Add listener to the button click event
        activateButton.onClick.AddListener(ToggleImageDetection);

        // Add listener to the close button click event
        closeButton.onClick.AddListener(CloseDialogPanel);
    }
    private void FixedUpdate()
    {
        // Detect mouse click or screen touch
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to detect if any object was clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is one of the objectsToShow
                for (int i = 0; i < objectsToShow.Length; i++)
                {
                    if (hit.transform == objectsToShow[i].transform)
                    {
                        // Call the OnObjectClicked function if the object was clicked
                        OnObjectClicked(i);
                    }
                }
            }
        }
    }
    // This method toggles image detection on and off
    void ToggleImageDetection()
    {
        isDetectionActive = !isDetectionActive;

        if (isDetectionActive)
        {
            // Enable all ImageTargetBehaviours to allow image detection
            foreach (var target in imageTargets)
            {
                target.enabled = true;
                // Subscribe to the target status change events
                target.OnTargetStatusChanged += OnTrackableStatusChanged;
            }

            Debug.Log("Image detection activated.");
        }
        else
        {
            // Disable all ImageTargetBehaviours to stop image detection
            foreach (var target in imageTargets)
            {
                target.enabled = false;
                // Unsubscribe from the target status change events
                target.OnTargetStatusChanged -= OnTrackableStatusChanged;
            }

            Debug.Log("Image detection deactivated.");

            // Hide all objects and the dialog panel since detection is deactivated
            foreach (var obj in objectsToShow)
            {
                obj.SetActive(false);
            }
            foreach (var obj in dialogPanel)
            {
                obj.SetActive(false);
            }
        }
    }

    // Event handler for tracking status changes
    void OnTrackableStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        var status = targetStatus.Status;

        // Find the index of the image target in the array
        int index = System.Array.IndexOf(imageTargets, behaviour);
        string detectedTargetName = behaviour.TargetName;
        Debug.Log($"Detected target: {detectedTargetName}");

        if (index >= 0)
        {
            // If the target is tracked or extended, show the corresponding object
            if (status == Status.TRACKED)
            {
                OnImageTargetDetected(index);
            }
            // If the target is lost (no pose or limited tracking), hide the corresponding object
            else if (status == Status.LIMITED || status == Status.NO_POSE||status==Status.EXTENDED_TRACKED)
            {
                OnImageTargetLost(index);
            }
        }
    }

    // Called when the image target is detected
    void OnImageTargetDetected(int index)
    {
        Debug.Log($"Image target {index} detected!");
        // Show the object corresponding to the detected image target
        objectsToShow[index].SetActive(true);
    }
    void OnObjectClicked(int index)
    {
        Debug.Log($"Object {index} clicked!");

        // If index is 0, initiate the white screen fade-in and show the dialog
        if (index == 0)
        {
            // Trigger the fade-in of the white screen
            //TurnScreenWhite();
            StartCoroutine(RotateObjectWhileScreenTurnsWhite(0));
            
            // Show the dialog after the white screen effect starts
            

        }

        // Hide the object when the dialog is shown
    }
    IEnumerator RotateObjectWhileScreenTurnsWhite(int index)
    {
        GameObject obj = objectsToShow[index];
        float rotationSpeed = 60f;  // Degrees per second
        float duration = 3.5f;  // The fade-in duration in seconds
        float elapsed = 0.0f;

        // Get the white screen panel and the image component
        Image whiteScreenImage = whiteScreenPanel.GetComponent<Image>();
        audioSource.clip = crystalballRotating;
        audioSource.loop = true;  // Set to loop in case the fade takes time
        audioSource.Play();  // Start playing the sound
        // Ensure the white screen panel is active and set its alpha to 0
        if (whiteScreenPanel != null)
        {
            whiteScreenPanel.SetActive(true);
            Color color = whiteScreenImage.color;
            color.a = 0.0f;
            whiteScreenImage.color = color;

            // Gradually increase the alpha and rotate the object at the same time
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;

                // Rotate the object during this time
                float rotationAmount = rotationSpeed * Time.deltaTime;
                obj.transform.Rotate(0f, rotationAmount, 0f);  // Rotating around Y-axis

                // Fade in the white screen (increase alpha)
                color.a = Mathf.Clamp01(elapsed / duration);
                whiteScreenImage.color = color;

                yield return null;  // Wait for the next frame
            }
            audioSource.Stop();  // Start playing the sound
            // Ensure the screen is fully white at the end
            color.a = 1.0f;
            whiteScreenImage.color = color;

            // Stop rotation at the end or continue depending on your preference
            obj.transform.rotation = Quaternion.Euler(0f, 360f, 0f);  // Ensure it completes the 360-degree rotation
            obj.SetActive(false);
            dialogPanel[index].SetActive(true);
        }
    }
    // Coroutine to rotate the object slowly for 360 degrees
    IEnumerator RotateObjectSlowly(GameObject obj)
    {
        float totalRotation = 0f;
        float rotationSpeed = 30f;  // Degrees per second

        while (totalRotation < 360f)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            obj.transform.Rotate(0f, rotationAmount, 0f);
            totalRotation += rotationAmount;

            yield return null;  // Wait for the next frame
        }
        obj.SetActive(false);
        // Ensure it completes exactly at 360 degrees and disappear
    }

    // Function to make the screen turn white
    void TurnScreenWhite()
    {
        // Assuming you have a full-screen UI panel (e.g., a white image) as a child of the Canvas
       
        if (whiteScreenPanel != null)
        {
            // Ensure the white screen panel is active (visible) for the fade-in effect
            whiteScreenPanel.SetActive(true);

            // Get the Image component of the panel to modify its color
            Image whiteScreenImage = whiteScreenPanel.GetComponent<Image>();

            if (whiteScreenImage != null)
            {
                // Start the fade-in coroutine
                StartCoroutine(FadeInWhiteScreen(whiteScreenImage));
            }
        }
    }
    // Coroutine to gradually fade in the white screen
    IEnumerator FadeInWhiteScreen(Image whiteScreenImage)
    {
        float duration = 2.0f;  // The fade-in duration in seconds
        float elapsed = 0.0f;

        Color color = whiteScreenImage.color;
        color.a = 0.0f;  // Set initial alpha to 0 (fully transparent)
        whiteScreenImage.color = color;

        // Gradually increase the alpha over time
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);  // Increase alpha value from 0 to 1
            whiteScreenImage.color = color;
            yield return null;  // Wait for the next frame
        }

        // Ensure the alpha is fully opaque after the fade-in
        color.a = 1.0f;
        whiteScreenImage.color = color;
    }
    // Called when the image target is lost
    void OnImageTargetLost(int index)
    {
        Debug.Log($"Image target {index} lost.");
        // Hide the object corresponding to the lost image target
        objectsToShow[index].SetActive(false);
        // Reset the object's rotation to its original state
        objectsToShow[index].transform.rotation = originalRotations[index];
    }

    // Update method to detect clicks on the objects
    void Update()
    {
        // Detect mouse click or screen touch
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to detect if any object was clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is one of the objectsToShow
                for (int i = 0; i < objectsToShow.Length; i++)
                {
                    if (hit.transform == objectsToShow[i].transform)
                    {
                        OnObjectClicked(i);
                    }
                }
            }
        }
    }
    // Called to close the dialog panel
    void CloseDialogPanel()
    {
        Debug.Log("Dialog panel closed!");
        // Hide the dialog panel
        foreach (var obj in dialogPanel)
        {
            obj.SetActive(false);
        }
    }

    // Cleanup the event subscription when the script is destroyed
    void OnDestroy()
    {
        foreach (var target in imageTargets)
        {
            if (target != null)
            {
                target.OnTargetStatusChanged -= OnTrackableStatusChanged;
            }
        }
    }
}
