using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
public class LockerRoomGlyphSpawner : MonoBehaviour
{
    [SerializeField] private GameObject glyphCheckPoint; // UI panel prefab (inactive at start)
    public Camera arCamera; // Assign the AR camera here
    private HitTestResult previousHit;
    private List<Vector3> clickPositions = new List<Vector3>();
    private int currentCheckpoint = 0;
    public Sprite clickedTouchPoint; // Assign the new Sprite in the Inspector
    public Sprite unclickedTouchPoint; // Assign the new Sprite in the Inspector
    public LineRenderer lineRenderer;
    public Button resetButton;
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip checkSound;       // The sound effect to play when the user taps the screen or clicks a button
    public AudioClip solvedSound;       // The sound effect to play when the user taps the screen or clicks a button
    private Boolean Solved = false;
    private void Start()
    {
        
    }
    // This method captures the hit test result from the place finder
    public void IntersectionLocation(HitTestResult intersection)
    {
        if (intersection != null)
        {
            previousHit = intersection;
        }
    }

    // This method activates the glyph panel at the specified location and deactivates the placeholder
    public void CreateGlyph()
    {
        if (previousHit != null)
        {
            clickPositions.Add(previousHit.Position);
            GameObject checkpoint = Instantiate(glyphCheckPoint, previousHit.Position,previousHit.Rotation);
        }
    }
}