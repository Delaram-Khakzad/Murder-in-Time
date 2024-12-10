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
    private bool Solved = false;
    private bool failed = false;
    public Camera arCamera; // Reference to the AR camera
    public Material newMaterial;
    public Material oldMaterial;

    private void Start()
    {
        // Initialization code if needed
        arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogError("MainCamera not found. Please ensure your AR camera is tagged as 'MainCamera'.");
            return;
        }
    }

    void Update()
    {
        if (Solved)
        {
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
                    // imageComponent = touchpoints[currentCheckpoint].GetComponent<Image>();
                    // imageComponent.sprite = clickedTouchPoint;
                    Renderer renderer = touchpoints[currentCheckpoint].GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material = newMaterial; // Assign a new material
                    }
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
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
    private void Fail() //What will happen after the user fails on solving
    {
        ResetCheckpoints(); //reset the glyph
        MidAirIndicator.SetActive(false);
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
            Renderer renderer = touchpoints[i].GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = oldMaterial; // Assign a new material
        }
        }
        lineRenderer.positionCount = 0;
        Solved = false;
    }
}
