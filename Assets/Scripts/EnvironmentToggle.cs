using UnityEngine;
using UnityEngine.UI;

public class EnvironmentToggle : MonoBehaviour
{
    public GameObject environment;   // Reference to the Environment object
    public Button toggleButton;      // The debug UI button for toggling visibility
    private bool isEnvironmentVisible = false;  // Track visibility state

    void Start()
    {
        // Add listener to the button
        toggleButton.onClick.AddListener(ToggleEnvironment);
        foreach (Transform child in environment.transform)
        {
            child.gameObject.SetActive(isEnvironmentVisible);
        }
    }

    // Method to toggle visibility of all child objects under Environment
    public void ToggleEnvironment()
    {
        isEnvironmentVisible = !isEnvironmentVisible;  // Toggle the state

        // Loop through each child object of Environment and set its active state
        foreach (Transform child in environment.transform)
        {
            child.gameObject.SetActive(isEnvironmentVisible);
        }
    }
}