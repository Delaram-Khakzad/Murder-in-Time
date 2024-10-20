using UnityEngine;
using UnityEngine.UI;

public class BookClickHandler : MonoBehaviour
{
    public GameObject uiImage;      // The UI Image to show when the book is clicked
    public Camera arCamera;         // The AR Camera from Vuforia
    public Button button;

    void Start()
    {
        // Ensure the UI image is hidden at the start
        uiImage.SetActive(false);
        button.onClick.AddListener(CloseImage);
    }

    void Update()
    {
        // Check if the user touches the screen
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // If the user taps the screen
            if (touch.phase == TouchPhase.Began)
            {
                // Cast a ray from the screen into the AR scene
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Perform a raycast and check if it hits the book
                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the object hit is the book
                    if (hit.collider.gameObject == gameObject)
                    {
                        Debug.Log("Book clicked!");

                        // Show the UI image
                        uiImage.SetActive(true);
                        button.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void CloseImage()
    {
        uiImage.SetActive(false);
        button.gameObject.SetActive(false);
    }
}