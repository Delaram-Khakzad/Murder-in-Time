using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTrace : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private List<Vector3> fingerPositions;
    private Boolean stop = false;
    public Camera arCamera;
    public Canvas worldCanvas; // 分配你的 World Space Canvas
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        fingerPositions = new List<Vector3>();
        lineRenderer.positionCount = 0; // Start with no positions
        //lineRenderer.startWidth = 50.0f;
        //lineRenderer.endWidth = 50.0f;
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
            Debug.Log("invalid");
            return; // Exit Update if no valid input
        }

        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == worldCanvas.gameObject || hit.collider.transform.IsChildOf(worldCanvas.transform))
            {
                Vector2 intersectionPoint = hit.point;
                //Debug.Log("射线与 Canvas 的交点在世界坐标：" + intersectionPoint);
                if (fingerPositions.Count == 0 || Vector2.Distance(fingerPositions[fingerPositions.Count - 1], intersectionPoint) > 0.1f)
                {

                    // Add the position to the list if it's sufficiently far from the last point
                    fingerPositions.Add(intersectionPoint);
                    lineRenderer.positionCount = fingerPositions.Count;
                    lineRenderer.SetPosition(fingerPositions.Count - 1, intersectionPoint);
                }
                // Optionally clear the line when the user lifts their finger or mouse button
                else if (Input.GetMouseButtonUp(0))
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
    }
}
