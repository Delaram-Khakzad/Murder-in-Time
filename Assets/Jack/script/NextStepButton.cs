using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextStepButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ToDeactivate; // Assign the panel you want to deactivate
    public GameObject[] ToActivate;   // Assign the panel you want to activate
    public Button nextButton;              // Button to close the highlight
    void Start()
    {
        nextButton.onClick.AddListener(Next);
    }
    void Next()
    {
        // Set ToDeactivate inactive
        if (ToDeactivate != null)
        {
            for (int i = 0; i < ToDeactivate.Length; i++) { ToDeactivate[i].SetActive(false); }

        }

        // Set ToActivate active
        if (ToActivate != null)
        {
            for (int i = 0; i < ToActivate.Length; i++) { ToActivate[i].SetActive(true); }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
