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
    public void AnchorCreator(Transform worldPosistioning) { 
        if(anchorExists)
        {
            return;
        }
        anchorOBJ.SetActive(true);
        anchorExists = true;
        anchorOBJ.transform.position=new Vector3(worldPosistioning.position.x, worldPosistioning.position.y, worldPosistioning.position.z+1);
        anchorOBJ.transform.rotation=Quaternion.Euler(0,0,0);
        Debug.Log(anchorOBJ.transform.rotation);
    }
}
