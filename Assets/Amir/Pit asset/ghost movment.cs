using UnityEngine;

public class SlowMove : MonoBehaviour
{
    // Speed of movement (adjustable from the Inspector)
    [SerializeField] private float speed = 0.1f;

    // Reference to the AudioSource component
    [SerializeField] private AudioSource audioSource;

    // Delay in seconds before audio starts playing
    [SerializeField] private float startDelay = 5f;

    // Distance at which the audio is at full volume
    [SerializeField] private float maxVolumeDistance = 0.1f;

    // Distance at which the audio becomes silent
    [SerializeField] private float minVolumeDistance = 1f;

    private bool audioStarted = false;
    private float timeElapsed = 0f;
    private Transform player;

    void Start()
    {
        // Find the player object by tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;

            // Set initial volume based on the starting distance
            float initialDistance = Vector3.Distance(player.position, transform.position);
            audioSource.volume = 1f - Mathf.Clamp01((initialDistance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
        }
        else
        {
            Debug.LogWarning("Player object with tag 'Player' not found.");
        }
    }

    void Update()
    {
        // Move the object slowly in the positive X direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Increase the elapsed time
        timeElapsed += Time.deltaTime;

        // Start the audio after the delay
        if (!audioStarted && timeElapsed >= startDelay)
        {
            audioSource.Play();
            audioStarted = true;
        }

        // Adjust the audio volume based on proximity to the player
        if (audioStarted && audioSource.isPlaying && player != null)
        {
            // Calculate the distance between the player and this object
            float distance = Vector3.Distance(player.position, transform.position);

            // Map the distance to a 0-1 range for volume adjustment
            float volume = 1f - Mathf.Clamp01((distance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
            audioSource.volume = volume;
        }
    }
}
