using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string scenename;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        SceneManager.LoadScene(scenename);
    }
}
