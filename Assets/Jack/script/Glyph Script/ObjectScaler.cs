using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject targetObject;   // The object to scale up
    public float scaleSpeed = 1f;     // Speed of scaling
    private bool isPressing = false;  // Flag to indicate if the button is being pressed
    private Vector3 originalScale;    // Store the initial scale of the object

    void Start()
    {
        if (targetObject != null)
        {
            // Save the initial scale of the object
            originalScale = targetObject.transform.localScale;
        }
        else
        {
            Debug.LogError("targetObject not assigned.");
        }
    }

    void Update()
    {
        if (isPressing && targetObject != null)
        {
            // Increase the scale of the object
            targetObject.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }

    // Called when the button is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
    }

    // Called when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }
}
