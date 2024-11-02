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
    public Transform[] checkpoints; // 在 Inspector 中按顺序分配检查点
    public GameObject[] touchpoints;
    private int currentCheckpoint = 0;
    private Image imageComponent;
    public Sprite clickedTouchPoint; // 在 Inspector 中分配已点击的 Sprite
    public Sprite unclickedTouchPoint; // 在 Inspector 中分配未点击的 Sprite
    public LineRenderer lineRenderer;
    public GameObject failReset;
    public AudioSource audioSource;  // 引用 AudioSource 组件
    public AudioClip checkSound;       // 用户点击屏幕或按钮时播放的音效
    public AudioClip solvedSound;      // 用户完成谜题时播放的音效
    public AudioClip failSound;      // 用户完成谜题时播放的音效
    public GameObject Glyph;
    public GameObject Canvas;
    public GameObject successText;
    private bool Solved = false;
    private bool failed = false;
    public Camera arCamera; // 引用您的 AR 相机

    private void Start()
    {

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
            failed= false;
        }
        Vector2 touchPosition;

        // 检查移动设备的触摸输入
        if (Input.touchCount > 0)
        {
            Debug.Log("touch");
            Touch touch = Input.GetTouch(0);
            touchPosition = touch.position;
        }
        // 或检查鼠标输入（用于桌面测试）
        else if (Input.GetMouseButton(0))
        {
            Debug.Log("click");
            touchPosition = Input.mousePosition;
        }
        else
        {
            Debug.Log("invalid");
            return; // 如果没有有效输入，退出 Update
        }

        // 将屏幕坐标转换为射线
        Ray ray = arCamera.ScreenPointToRay(touchPosition);

        // 执行射线检测，获取所有命中的碰撞体
        RaycastHit[] hits = Physics.RaycastAll(ray);

        bool checkpointHit = false;

        foreach (RaycastHit hit in hits)
        {
            // 检查命中的碰撞体是否具有 "Checkpoint" 标签
            if (hit.collider.CompareTag("Checkpoint"))
            {
                for(int i=currentCheckpoint+1;i<checkpoints.Length;i++)
                {
                    if(hit.collider.transform == checkpoints[i])
                    {
                        failed = true;
                        return;
                    }
                }
                // 检查命中的碰撞体是否为当前的检查点
                if (hit.collider.transform == checkpoints[currentCheckpoint])
                {
                    checkpointHit = true;
                    Debug.Log($"Checkpoint {currentCheckpoint + 1} reached!");
                    imageComponent = touchpoints[currentCheckpoint].GetComponent<Image>();
                    imageComponent.sprite = clickedTouchPoint;
                    currentCheckpoint++; // 移动到下一个检查点

                    if (audioSource != null && checkSound != null)
                    {
                        audioSource.PlayOneShot(checkSound);  // 播放点击音效
                    }

                    if (currentCheckpoint >= checkpoints.Length)
                    {
                        Debug.Log("Puzzle Solved!");
                        Glyph.SetActive(false);
                        successText.SetActive(true);
                        Solved = true;
                        Canvas.SetActive(false);
                        if (audioSource != null && solvedSound != null)
                        {
                            audioSource.PlayOneShot(solvedSound);  // 播放完成音效
                        }
                        lineRenderer.GetComponent<FingerTrace>().stopdrawing();
                        // 在此触发谜题完成的逻辑
                        // ResetCheckpoints(); // 可选择在完成后重置
                    }

                    break; // 找到目标检查点后退出循环
                }

            }
        }

        if (!checkpointHit)
        {
            Debug.Log("No checkpoint hit by Raycast.");
        }
    }
    private void Fail()
    {
        ResetCheckpoints();
        audioSource.PlayOneShot(failSound);  // 播放点击音效
    }
    public void startAgain()
    {
        Glyph.SetActive(true);
        failReset.SetActive(false );
        audioSource.Stop();
    }
    private void ResetCheckpoints()
    {
        currentCheckpoint = 0; // 重置检查点计数器
        lineRenderer.positionCount = 0;
        for (int i = 0; i < touchpoints.Length; i++)
        {
            touchpoints[i].GetComponent<Image>().sprite = unclickedTouchPoint;
        }
        lineRenderer.positionCount = 0;
        Solved = false;
    }

}