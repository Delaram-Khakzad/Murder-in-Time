using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TheBeginning : MonoBehaviour
{
    public GameObject fullScreenDimPanel; // The full screen dimming panel
    public GameObject highlightPanel; // The panel to highlight
    public GameObject nextButton; // The panel to highlight
    public GameObject textPanel; // The panel to highlight
    public GameObject textBackground; // The panel to highlight
    public GameObject nButton; // The panel to highlight
    public TextMeshProUGUI highlightText; // Text component to show introduction
    public string introductionText = "This is the introduction to the highlighted panel."; // Default introduction text
    public float dimDuration = 1f; // Duration of dimming effect
    public Color dimColor = new Color(0, 0, 0, 0.7f); // Default dim color
    public GameObject[] ToActivate;   // Assign the panel you want to activate
    private Color originalDimColor;
    public Button closeButton;              // Button to close the highlight
    void Start()
    {
        // Set the dim panel initially to be invisible
        fullScreenDimPanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        originalDimColor = fullScreenDimPanel.GetComponent<Image>().color;

        // Initially hide the highlight panel
        highlightPanel.SetActive(false);
        ShowHighlightPanel();
        if (closeButton != null)
        {
            // Assign the CloseHighlight method to the button's OnClick event
            closeButton.onClick.AddListener(HideHighlightPanel);
        }
    }

    // Function to dim the screen and highlight the panel
    public void ShowHighlightPanel()
    {
        StartCoroutine(DimAndHighlight());
    }

    // Coroutine to fade the dimming effect in and then show the highlighted panel
    IEnumerator DimAndHighlight()
    {
        // Activate the dim panel and fade it in
        fullScreenDimPanel.SetActive(true);

        float elapsedTime = 0;
        Image dimImage = fullScreenDimPanel.GetComponent<Image>();

        while (elapsedTime < dimDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, dimColor.a, elapsedTime / dimDuration);
            dimImage.color = new Color(dimColor.r, dimColor.g, dimColor.b, alpha);
            yield return null;
        }

        // Show the highlighted panel with the introduction
        highlightPanel.SetActive(true);
        textBackground.SetActive(true);
        highlightText.text = introductionText;
        nButton.SetActive(true);
        nextButton.SetActive(true);
    }

    // Function to hide the dim panel and the highlighted panel
    public void HideHighlightPanel()
    {
        StartCoroutine(UndimAndHide());
    }

    // Coroutine to fade out the dimming effect and hide the panels
    IEnumerator UndimAndHide()
    {
        float elapsedTime = 0;
        Image dimImage = fullScreenDimPanel.GetComponent<Image>();

        while (elapsedTime < dimDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(dimColor.a, 0, elapsedTime / dimDuration);
            dimImage.color = new Color(dimColor.r, dimColor.g, dimColor.b, alpha);
            // Set ToActivate active

            yield return null;
        }
        if (ToActivate != null)
        {
            for (int i = 0; i < ToActivate.Length; i++) { ToActivate[i].SetActive(true); }
        }
        // Disable both panels after undimming
        textPanel.SetActive(false);

    }
}
