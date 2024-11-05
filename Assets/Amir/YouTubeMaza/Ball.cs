using UnityEngine;
 
public class Ball : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    void Update()
    {
        if ((gameObject.GetComponent<Rigidbody>().velocity.x > 0.01 ||
          gameObject.GetComponent<Rigidbody>().velocity.y > 0.01) &&
          !gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
        else if (gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Pause();
 
        }
    }
}
 
