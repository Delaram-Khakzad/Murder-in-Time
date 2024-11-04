using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Drag your VideoPlayer component here in the Inspector
    public Button button;            // Drag the Button UI element here
    public VideoPlayer secondVideoPlayer; // For the second video

    void Start()
    {
        // Initially hide the button
        button.gameObject.SetActive(true);

        // Subscribe to the event when the video finishes playing
        videoPlayer.loopPointReached += OnVideoEnd;
        
        // Add a listener to the button's onClick event to play the second video
        button.onClick.AddListener(PlayNextVideo);
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Show the button when the video finishes
        button.gameObject.SetActive(true);
    }

    void PlayNextVideo()
    {
        videoPlayer.gameObject.SetActive(false);
        // Hide the button, then play the second video
        button.gameObject.SetActive(false);

        // Activate the second video player's GameObject
        secondVideoPlayer.gameObject.SetActive(true);

        secondVideoPlayer.Play();
    }
}