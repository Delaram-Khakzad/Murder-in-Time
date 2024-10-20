using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchS3toS4 : MonoBehaviour {

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "S3toS4") 
        {
            SceneManager.LoadScene(4);
        }
    }
}