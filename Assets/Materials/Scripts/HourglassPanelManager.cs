using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class HourglassPanelManager : MonoBehaviour
{
    public GameObject panel;     // Reference to the panel to close
    public Button classroomButton;
    public Button lockerButton;
    public Button hallwayButton;
    public VideoPlayer videoPlayer;   // Reference to a single VideoPlayer component
    private string targetScene;       // The scene to load after the video finishes

    private void Start()
    {
        // Add listeners for each button to call the respective scene loading functions
        classroomButton.onClick.AddListener(() => PlayVideoAndLoadScene("Scene3"));
        lockerButton.onClick.AddListener(() => PlayVideoAndLoadScene("Scene4"));
        hallwayButton.onClick.AddListener(() => PlayVideoAndLoadScene("Scene5"));

        // Ensure the panel is hidden initially
        panel.SetActive(false);

        // Subscribe to the VideoPlayer's loopPointReached event to know when the video ends
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.gameObject.SetActive(false); // Hide video player initially
    }

    private void PlayVideoAndLoadScene(string sceneName)
    {
        // Set the target scene to load after the video
        targetScene = sceneName;

        // Hide the panel and start playing the video
        panel.SetActive(false);
        videoPlayer.gameObject.SetActive(true); // Show the video player
        videoPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Hide the video player after the video finishes
        videoPlayer.gameObject.SetActive(false);

        // Load the target scene
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the video player event to avoid memory leaks
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}