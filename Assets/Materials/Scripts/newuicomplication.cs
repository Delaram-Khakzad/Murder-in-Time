using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class UIcompletion : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Drag your VideoPlayer component here in the Inspector
    public Button button;            // Drag the Button UI element here
    public GameObject panel; 
    public GameObject LensIcon;
    public GameObject LensIconBorder;
    public GameObject WhoBorder;
    public GameObject WhyBorder;
    public GameObject HowBorder;

    void Start()
    {
        LensIcon.SetActive(true);
        LensIconBorder.SetActive(true);
        WhoBorder.SetActive(true);
        WhyBorder.SetActive(true);
        HowBorder.SetActive(true);
        button.gameObject.SetActive(true);
        button.onClick.AddListener(UIcomplete);
    }

    void UIcomplete()
    {
        videoPlayer.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        panel.SetActive(false);
    }
}