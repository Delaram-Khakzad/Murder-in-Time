using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using Image = UnityEngine.UI.Image;

public class Pathtracing : MonoBehaviour
{
    public Transform[] checkpoints; // Assign checkpoints in sequence in the Inspector
    public GameObject[] touchpoints;  
    private int currentCheckpoint = 0;
    private Image imageComponent;    
    public Sprite clickedTouchPoint; // Assign the sprite for a clicked touchpoint in the Inspector
    public Sprite unclickedTouchPoint; // Assign the sprite for an unclicked touchpoint in the Inspector
    public LineRenderer lineRenderer;  
    public GameObject failReset;    //For user to redo the glyph after they fail
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip checkSound;     // Sound to play when the user clicks the screen or button
    public AudioClip solvedSound;    // Sound to play when the user solves the puzzle
    public AudioClip failSound;      // Sound to play when the user fails the puzzle
    public GameObject Glyph;         
    public GameObject Canvas;
    public GameObject successText;
    public GameObject MidAirIndicator;
    public GameObject documents;
    public GameObject message;
    public AudioClip messageAudioForMurder; //voice aids
    
    private bool Solved = false;
    private bool failed = false;
    public Camera arCamera; // Reference to the AR camera

    private void Start()
    {
        // Initialization code if needed
    }
    // Check if there is a valid touch input
    private bool IsTouchInput()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
    }

    // Get the position of the touch or mouse click
    private Vector3 GetInputPosition()
    {
        if (Input.touchCount > 0)
        {
            return Input.GetTouch(0).position; // Touch position
        }
        return Input.mousePosition; // Mouse click position
    }
    void Update()
    {
        if (Solved)
        {
            // Detect user input (mouse click or touch)
            if (Input.GetMouseButtonDown(0) || IsTouchInput())
            {
                // Cast a ray from the touch or click position
                Vector3 inputPosition = GetInputPosition();

                Ray rayforDocument = arCamera.ScreenPointToRay(inputPosition);
                RaycastHit hit;

                // Check if the ray hits an object
                if (Physics.Raycast(rayforDocument, out hit))
                {

                    // Check if the clicked/touched object is the current object
                    if (hit.collider.gameObject == documents)
                    {
                        Debug.Log("hit documents");
                        // Activate the target object
                        if (message != null)
                        {
                            message.SetActive(true);
                            audioSource.clip =messageAudioForMurder;
                            audioSource.Play();
                        }
                        else
                        {
                            Debug.LogWarning("Target object is not assigned!");
                        }
                    }
                }
            }
            return;
        }
        if (failed)
        {
            Fail();
            Glyph.SetActive(false);
            failReset.SetActive(true);
            failed = false;
        }
        Vector2 touchPosition;

        // Check for touch input on a mobile device
        if (Input.touchCount > 0)
        {
            Debug.Log("touch");
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
        }
        // Or check for mouse input (useful for desktop testing)
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("click");
            touchPosition = Input.mousePosition;
        }
        else
        {
            Debug.Log("invalid");
            return; // Exit Update if no valid input is detected
        }

        // Convert screen coordinates to a ray
        Ray ray = arCamera.ScreenPointToRay(touchPosition);

        // Perform raycast to get all colliders hit by the ray
        RaycastHit[] hits = Physics.RaycastAll(ray);

        bool checkpointHit = false;

        foreach (RaycastHit hit in hits)
        {
            // Check if the hit collider has the "Checkpoint" tag
            if (hit.collider.CompareTag("Checkpoint"))
            {
                for (int i = currentCheckpoint + 1; i < checkpoints.Length; i++)
                {
                    if (hit.collider.transform == checkpoints[i])
                    {
                        failed = true;
                        return;
                    }
                }
                // Check if the hit collider is the current checkpoint
                if (hit.collider.transform == checkpoints[currentCheckpoint])
                {
                    checkpointHit = true;
                    Debug.Log($"Checkpoint {currentCheckpoint + 1} reached!");
                    imageComponent = touchpoints[currentCheckpoint].GetComponent<Image>();
                    imageComponent.sprite = clickedTouchPoint;
                    currentCheckpoint++; // Move to the next checkpoint

                    if (audioSource != null && checkSound != null)
                    {
                        audioSource.PlayOneShot(checkSound);  // Play click sound
                    }

                    if (currentCheckpoint >= checkpoints.Length)
                    {
                        succeed();
                        if (audioSource != null && solvedSound != null)
                        {
                            audioSource.PlayOneShot(solvedSound);  // Play puzzle solved sound
                        }
                        lineRenderer.GetComponent<FingerTrace>().stopdrawing();
                        // Trigger puzzle completion logic here
                        // ResetCheckpoints(); // Optionally reset after completion
                    }

                    break; // Exit loop after finding the target checkpoint
                }
            }
        }

        if (!checkpointHit)
        {
            Debug.Log("No checkpoint hit by Raycast.");
        }
    }

    //if user solves the glyph
    private void succeed()
    {
        Debug.Log("Puzzle Solved!");
        Glyph.SetActive(false); 
        successText.SetActive(true); //notification about success
        Solved = true;
        Canvas.SetActive(false);
        MidAirIndicator.SetActive(false);
    }
    private void Fail() //What will happen after the user fails on solving
    {
        ResetCheckpoints(); //reset the glyph
        audioSource.PlayOneShot(failSound);  // Play fail sound
    }
    public void startAgain() //After failure, user can click the failReset button to solve again.
    {
        Glyph.SetActive(true);
        failReset.SetActive(false);
        audioSource.Stop();
    }
    //reset the glyph
    private void ResetCheckpoints()
    {
        currentCheckpoint = 0; // Reset checkpoint counter
        lineRenderer.positionCount = 0;
        for (int i = 0; i < touchpoints.Length; i++)
        {
            touchpoints[i].GetComponent<Image>().sprite = unclickedTouchPoint;
        }
        lineRenderer.positionCount = 0;
        Solved = false;
    }
}
