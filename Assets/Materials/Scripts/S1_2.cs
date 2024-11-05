using UnityEngine;
using UnityEngine.SceneManagement;

public class S1_2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Load the next scene (replace "Scene1" with the exact name of your scene)
            SceneManager.LoadScene("Scene2");
        }
    }
}

