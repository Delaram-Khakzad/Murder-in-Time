using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerTrace : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> fingerPositions;
    private bool stop = false;
    public Camera arCamera;
    public Canvas worldCanvas; // Assign your World Space Canvas here
    public GameObject cursor; // Assign your cursor object here

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        fingerPositions = new List<Vector3>();
        lineRenderer.positionCount = 0; // Initially, there are no points
        
        arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogError("MainCamera not found. Please ensure your AR camera is tagged as 'MainCamera'.");
            return;
        }
            cursor.SetActive(false); // Hide the cursor initially
        }

    public void stopdrawing()
    {
        stop = true;
    }

    void Update()
    {
        if (stop)
        {
            return;
        }

        Vector2 touchPosition;

        // Check for touch input on a mobile device
        if (Input.touchCount > 0)
        {
            Debug.Log("touch");
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            cursor.SetActive(true); // Show cursor when touch starts
        }
        // Check for mouse input (useful for desktop testing)
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("click");
            touchPosition = Input.mousePosition;
            cursor.SetActive(true); // Show cursor when click starts
        }
        else
        {
            Debug.Log("invalid");
            cursor.SetActive(false); // Hide the cursor
            return; // Exit Update if no valid input is detected
        }

        // Create a ray from the screen point
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hit the Canvas or its child objects
            if (hit.collider.gameObject == worldCanvas.gameObject || hit.collider.transform.IsChildOf(worldCanvas.transform))
            {
                Vector3 intersectionPoint = hit.point;
                cursor.transform.position = intersectionPoint;
                // Add a new point if it's far enough from the last point
                if (fingerPositions.Count == 0 || Vector3.Distance(fingerPositions[fingerPositions.Count - 1], intersectionPoint) > 0.1f)
                {
                    fingerPositions.Add(intersectionPoint);
                    lineRenderer.positionCount = fingerPositions.Count;
                    lineRenderer.SetPosition(fingerPositions.Count - 1, intersectionPoint);
                }

                // Optional: Clear the line when the user releases the touch or mouse button
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("clear");
                    ClearLine();
                }
            }
        }
    }

    void ClearLine()
    {
        fingerPositions.Clear();
        lineRenderer.positionCount = 0;
        cursor.gameObject.SetActive(false);
    }
}
