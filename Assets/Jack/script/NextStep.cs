using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStep : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ToDeactivate; // Assign the panel you want to deactivate
    public GameObject[] ToActivate;   // Assign the panel you want to activate
    void Start()
    {
        // Set ToDeactivate inactive
        if (ToDeactivate != null)
        {
            for(int i = 0; i < ToDeactivate.Length; i++) { ToDeactivate[i].SetActive(false); }
           
        }

        // Set ToActivate active
        if (ToActivate != null)
        {
            for( int i = 0;i < ToActivate.Length;i++) { ToActivate[i].SetActive(true); }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
