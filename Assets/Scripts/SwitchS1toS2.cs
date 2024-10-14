using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "S1toS2") 
        {
            SceneManager.LoadScene(2);
        }
    }
}