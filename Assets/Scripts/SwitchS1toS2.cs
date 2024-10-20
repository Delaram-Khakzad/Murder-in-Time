using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchS1toS2 : MonoBehaviour {

    public GameObject quad;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "S1toS2") 
        {
            SceneManager.LoadScene(2);
        }

        if(other.tag == "showBoard") 
        {
            quad.SetActive(true);
        }
    }
}