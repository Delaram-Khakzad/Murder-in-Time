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
    public Transform[] checkpoints; // Assign checkpoints in order in the Inspector
    public GameObject[] touchpoints;
    private int currentCheckpoint = 0;
    private Image imageComponent;
    public Sprite clickedTouchPoint; // Assign the new Sprite in the Inspector
    public Sprite unclickedTouchPoint; // Assign the new Sprite in the Inspector
    public LineRenderer lineRenderer;
    public Button resetButton;
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip checkSound;       // The sound effect to play when the user taps the screen or clicks a button
    public AudioClip solvedSound;       // The sound effect to play when the user taps the screen or clicks a button
    public GameObject Glyph;
    public GameObject successText;
    private Boolean Solved = false;
    private void Start()
    {
        

        resetButton.onClick.AddListener(ResetCheckpoints);
    }
    void Update()
    {
        if(Solved )
        {
            return;
        }
        Vector2 touchPosition;

        // Check for mobile touch input
        if (Input.touchCount > 0)
        {
            Debug.Log("touch");
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
        }
        // Or check for mouse input (for desktop testing)
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("click");
            touchPosition = Input.mousePosition;
        }
        else
        {
            Debug.Log("invalid") ;
            return; // Exit Update if no valid input
        }

        // Set Z position to 0 for 2D coordinates


        // Perform a 2D raycast at the touch position
       
        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Checkpoint"))
        {
            // Check if the hit collider is the current checkpoint
            if (hit.collider.transform == checkpoints[currentCheckpoint])
            {
                Debug.Log($"Checkpoint {currentCheckpoint + 1} reached!");
                imageComponent = touchpoints[currentCheckpoint].GetComponent<Image>();
                imageComponent.sprite = clickedTouchPoint;
                currentCheckpoint++; // Move to the next checkpoint
                if (audioSource != null && checkSound != null)
                {
                    audioSource.PlayOneShot(checkSound);  // Play the assigned tap sound
                }
                if (currentCheckpoint >= checkpoints.Length)
                {
                    Debug.Log("Puzzle Solved!");
                    Glyph.SetActive(false) ;
                    successText.SetActive(true) ;
                    Solved = true;
                    audioSource.PlayOneShot(solvedSound);  // Play the assigned tap sound
                    lineRenderer.GetComponent<FingerTrace>().stopdrawing();
                    // Trigger puzzle completion logic here
                    //ResetCheckpoints(); // Optionally reset after solving
                }
            }
        }
    }

    private void ResetCheckpoints()
    {
        currentCheckpoint = 0; // Reset the checkpoint counter
        lineRenderer.positionCount = 0;
        for (int i = 0; i < touchpoints.Length; i++)
        {
            touchpoints[i].GetComponent<Image>().sprite = unclickedTouchPoint; 
            
        }
        lineRenderer.positionCount = 0;
        Solved = false;
    }
}
