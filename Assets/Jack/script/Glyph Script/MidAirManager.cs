using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MidAirManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject anchorOBJ;
    private Boolean anchorExists = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AnchorCreator(Transform worldPositioning) {
        if (anchorExists)
        {
            return;
        }
        anchorOBJ.SetActive(true);
        anchorExists = true;

        // set position in front of AR camera
        anchorOBJ.transform.position = worldPositioning.position + worldPositioning.forward;

        // rotation with AR camera
        anchorOBJ.transform.rotation = worldPositioning.rotation;

        Debug.Log(anchorOBJ.transform.rotation);
    }
}
