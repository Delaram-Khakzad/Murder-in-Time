using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SwitchS0toS1 : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Drag your VideoPlayer component here in the Inspector
    public Button button;            // Drag the Button UI element here
    public GameObject image;         // Drag the Image UI element here

    void Start()
    {
        // Initially hide the button
        image.SetActive(false);
        button.gameObject.SetActive(true);

        // Subscribe to the event when the video finishes playing
        videoPlayer.loopPointReached += OnVideoEnd;
        
        // Add a listener to the button's onClick event to play the second video
        button.onClick.AddListener(S0toS1);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Show the button when the video finishes
        image.SetActive(true);
        button.gameObject.SetActive(true);
    }

    void S0toS1()
    {
        videoPlayer.gameObject.SetActive(false);
        image.SetActive(false);
        // Hide the button, then play the second video
        button.gameObject.SetActive(false);

        SceneManager.LoadScene(1);
    }
}