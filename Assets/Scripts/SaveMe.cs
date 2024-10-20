using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMe : MonoBehaviour {

    private void Start() 
    {
        DontDestroyOnLoad(gameObject);
    }
}