using UnityEngine;
using Vuforia;

public class ShowObjectsForLimitedTime : MonoBehaviour
{
    public GameObject object1; // Drag your first object here (shows for 5 seconds)
    public GameObject object2; // Drag your second object here (shows for 1 minute)
    
    public float object1ShowTime = 5f;  // Time to show object1 (5 seconds)
    public float object2ShowTime = 60f; // Time to show object2 (1 minute)

    private bool isTimerRunning1 = false;
    private bool isTimerRunning2 = false;
    private float timer1 = 0f;
    private float timer2 = 0f;

    // This will handle Vuforia's tracking events
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
        // Ensure the objects are invisible at the start of the game
        HideObjects();
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // When the target is tracked, show objects if the timers aren't running
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
        }
    }

    private void ShowObject1()
    {
        object1.SetActive(true); // Make object1 visible
    }

    private void ShowObject2()
    {
        object2.SetActive(true); // Make object2 visible
    }

    private void HideObject1()
    {
        object1.SetActive(false); // Hide object1
    }

    private void HideObject2()
    {
        object2.SetActive(false); // Hide object2
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

    private void Update()
    {
        // Timer for object1 (5 seconds)
        if (isTimerRunning1)
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
            {
                HideObject1();
                isTimerRunning1 = false;
            }
        }

        // Timer for object2 (1 minute)
        if (isTimerRunning2)
        {
            timer2 -= Time.deltaTime;
            if (timer2 <= 0)
            {
                HideObject2();
                isTimerRunning2 = false;
            }
        }
    }

    private void HideObjects()
    {
        object1.SetActive(false); // Hide object1 at the start
        object2.SetActive(false); // Hide object2 at the start
    }
}
