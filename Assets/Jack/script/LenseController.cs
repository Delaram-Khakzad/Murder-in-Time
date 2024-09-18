using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LenseController : MonoBehaviour
{
    public Button toggleButton;  // Button that triggers the color toggle
    public Image panelImage;     // The Image component of the Panel (UI element)
    private bool LenseOn = false;  // Tracks if the panel currently has a red border

    // Stores the original border color (the initial color of the panel)
    private Color originalBorderColor;
    private Color LenseColor = new (252f / 255f, 253f / 255f, 209f / 255f);
    void Start()
    {
        // Store the initial color of the panel
        originalBorderColor = panelImage.color;

        // Initialize the panel as transparent
        SetPanelTransparent();

        // Add a listener to the button click event to toggle the border color
        toggleButton.onClick.AddListener(ToggleBorder);
    }

    // Toggles between a red border and the original transparent state
    void ToggleBorder()
    {
        if (LenseOn)
        {
            // Restore the panel to its original transparent state
            SetPanelTransparent();
        }
        else
        {
            // Change the panel border to red
            OpenLense();
        }

        // Flip the state to indicate whether the panel is in red border mode
        LenseOn = !LenseOn;
    }

    // Sets the panel to be transparent
    void SetPanelTransparent()
    {
        Color transparentColor = originalBorderColor;
        transparentColor.a = 0f;  // Set alpha to 0, making the panel fully transparent
        panelImage.color = transparentColor;
    }

    // Sets the panel's border color to red
    void OpenLense()
    {
        Color ActiveLenseBorder = LenseColor;
        ActiveLenseBorder.a = 0.2f; //Set alpha to 1, making the panel fully opaque with a red color
        panelImage.color = ActiveLenseBorder;
    }
}
