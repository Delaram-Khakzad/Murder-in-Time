using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UIcompletion : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Drag your VideoPlayer component here in the Inspector
    public Button button;            // Drag the Button UI element here
    public GameObject endImage;         // Drag the endImage UI element here
    public GameObject panel; 
    public GameObject LensIcon;
    public GameObject LensIconBorder;

    void Start()
    {
        // Initially hide the button
        endImage.SetActive(false);
        LensIcon.SetActive(true);
        LensIconBorder.SetActive(true);
        button.gameObject.SetActive(false);

        // Subscribe to the event when the video finishes playing
        videoPlayer.loopPointReached += OnVideoEnd;
        
        // Add a listener to the button's onClick event to play the second video
        button.onClick.AddListener(UIcomplete);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Show the button when the video finishes
        endImage.SetActive(true);
        button.gameObject.SetActive(true);
    }

    void UIcomplete()
    {
        videoPlayer.gameObject.SetActive(false);
        endImage.SetActive(false);
        // Hide the button, then play the second video
        button.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}