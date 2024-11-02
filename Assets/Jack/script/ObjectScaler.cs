using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject targetObject;   // 需要变大的对象
    public float scaleSpeed = 1f;     // 缩放速度
    private bool isPressing = false;  // 标记按钮是否被按下
    private Vector3 originalScale;    // 保存对象的初始尺寸

    void Start()
    {
        if (targetObject != null)
        {
            // 保存对象的初始尺寸
            originalScale = targetObject.transform.localScale;
        }
        else
        {
            Debug.LogError("未分配 targetObject。");
        }
    }

    void Update()
    {
        if (isPressing && targetObject != null)
        {
            // 增加对象的缩放比例
            targetObject.transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }

    // 按钮按下时调用
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
    }

    // 按钮松开时调用
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }
}