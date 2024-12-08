using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerEventController : MonoBehaviour
{
    public GameObject midairpositoner;

    void Update()
    {
        Camera arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.Log("no camera");
        }
        // Detect user input (mouse click or touch)
        if (Input.GetMouseButtonDown(0) || IsTouchInput())
        {
            // Cast a ray from the touch or click position
            Vector3 inputPosition = GetInputPosition();
            
            Ray ray = arCamera.ScreenPointToRay(inputPosition);
            RaycastHit hit;

            // Check if the ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                
                // Check if the clicked/touched object is the current object
                if (hit.collider.gameObject == gameObject)
                {
                    Debug.Log("hit gameobject");
                    // Activate the target object
                    if (midairpositoner != null)
                    {
                        midairpositoner.SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning("Target object is not assigned!");
                    }
                }
            }
        }
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
}
