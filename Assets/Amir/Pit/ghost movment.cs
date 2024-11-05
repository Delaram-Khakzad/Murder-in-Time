using UnityEngine;

public class SlowMove : MonoBehaviour
{
    // Speed of movement (adjustable from the Inspector)
    [SerializeField] private float speed = 0.1f;

    // Reference to the AudioSource component
    [SerializeField] private AudioSource audioSource;

    void Update()
    {
        // Move the object slowly in the positive X direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Play the audio source if a collision occurs
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
