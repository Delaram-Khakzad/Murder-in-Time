using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchS2toS3 : MonoBehaviour {

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "S2toS3") 
        {
            SceneManager.LoadScene(3);
        }
    }
}