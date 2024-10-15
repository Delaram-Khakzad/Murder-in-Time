using UnityEngine;
using Vuforia;

public class ShowObjectsForLimitedTime : MonoBehaviour
{
    public GameObject object1; // First object (e.g., 5 seconds)
    public GameObject object2; // Second object (e.g., 1 minute)
    public GameObject object3; // Third object (e.g., 30 seconds)

    public float object1ShowTime = 5f;   // Time to show object1 (in seconds)
    public float object2ShowTime = 60f;  // Time to show object2 (in seconds)
    public float object3ShowTime = 30f;  // Time to show object3 (in seconds)

    private bool isTimerRunning1 = false;
    private bool isTimerRunning2 = false;
    private bool isTimerRunning3 = false;

    private float timer1 = 0f;
    private float timer2 = 0f;
    private float timer3 = 0f;

    private void OnEnable()
    {
        var observer = GetComponent<ObserverBehaviour>();
        if (observer)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnDisable()
    {
        var observer = GetComponent<ObserverBehaviour>();
        if (observer)
        {
            observer.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }

    private void Start()
    {
        HideObjects(); // Ensure all objects are invisible at the start
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED)
        {
            if (!isTimerRunning1)
            {
                ShowObject1();
                StartTimer1();
            }
            if (!isTimerRunning2)
            {
                ShowObject2();
                StartTimer2();
            }
            if (!isTimerRunning3)
            {
                ShowObject3();
                StartTimer3();
            }
        }
    }

    private void Update()
    {
        if (isTimerRunning1)
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
            {
                HideObject1();
                isTimerRunning1 = false;
            }
        }

        if (isTimerRunning2)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                HideObject2();
                isTimerRunning2 = false;
            }
        }

        if (isTimerRunning3)
        {
            timer3 -= Time.deltaTime;
            if (timer3 <= 0)
            {
                HideObject3();
                isTimerRunning3 = false;
            }
        }
    }

    private void ShowObject1()
    {
        object1.SetActive(true);
    }

    private void ShowObject2()
    {
        object2.SetActive(true);
    }

    private void ShowObject3()
    {
        object3.SetActive(true);
    }

    private void HideObject1()
    {
        object1.SetActive(false);
    }

    private void HideObject2()
    {
        object2.SetActive(false);
    }

    private void HideObject3()
    {
        object3.SetActive(false);
    }

    private void StartTimer1()
    {
        isTimerRunning1 = true;
        timer1 = object1ShowTime;
    }

    private void StartTimer2()
    {
        isTimerRunning2 = true;
        timer2 = object2ShowTime;
    }

    private void StartTimer3()
    {
        isTimerRunning3 = true;
        timer3 = object3ShowTime;
    }

    private void HideObjects()
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
    }
}