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
    public Canvas worldCanvas; // 分配你的 World Space Canvas
    public GameObject cursor; // 分配你的光标对象

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        fingerPositions = new List<Vector3>();
        lineRenderer.positionCount = 0; // 初始没有点

        cursor.SetActive(false); // 初始隐藏光标
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

        // 检查移动设备的触摸输入
        if (Input.touchCount > 0)
        {
            Debug.Log("touch");
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
            cursor.SetActive(true); // 当触摸开始时显示光标
        }
        // 检查鼠标输入（用于桌面测试）
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("click");
            touchPosition = Input.mousePosition;
            cursor.SetActive(true); // 当点击开始时显示光标
        }
        else
        {
            Debug.Log("invalid");
            cursor.SetActive(false); // 隐藏光标
            return; // 如果没有有效输入，退出 Update
        }

        // 从屏幕点创建一条射线
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            // 检查射线是否击中了 Canvas 或其子对象
            if (hit.collider.gameObject == worldCanvas.gameObject || hit.collider.transform.IsChildOf(worldCanvas.transform))
            {
                Vector3 intersectionPoint = hit.point;
                cursor.transform.position = intersectionPoint;
                // 如果距离上一个点足够远，则添加新点
                if (fingerPositions.Count == 0 || Vector3.Distance(fingerPositions[fingerPositions.Count - 1], intersectionPoint) > 0.1f)
                {
                    fingerPositions.Add(intersectionPoint);
                    lineRenderer.positionCount = fingerPositions.Count;
                    lineRenderer.SetPosition(fingerPositions.Count - 1, intersectionPoint);
                }

                // 可选：当用户抬起手指或鼠标按钮时清除线条
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
