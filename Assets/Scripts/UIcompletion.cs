using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UIcompletion : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Drag your VideoPlayer component here in the Inspector
    public Button button;            // Drag the Button UI element here
    public GameObject image;         // Drag the Image UI element here
    public GameObject panel; 

    void Start()
    {
        // Initially hide the button
        image.SetActive(false);
        button.gameObject.SetActive(false);

        // Subscribe to the event when the video finishes playing
        videoPlayer.loopPointReached += OnVideoEnd;
        
        // Add a listener to the button's onClick event to play the second video
        button.onClick.AddListener(UIcomplete);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Show the button when the video finishes
        image.SetActive(true);
        button.gameObject.SetActive(true);
    }

    void UIcomplete()
    {
        videoPlayer.gameObject.SetActive(false);
        image.SetActive(false);
        // Hide the button, then play the second video
        button.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}