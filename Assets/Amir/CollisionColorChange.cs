using UnityEngine;

public class SimpleCollisionColorChange : MonoBehaviour
{
    public AudioSource audioSource;  // Assign an AudioSource in the Inspector

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody (assumed to be the player)
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            // Change color to red
            ChangeColorToRed();

            // Play sound if AudioSource is assigned
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void ChangeColorToRed()
    {
        Renderer objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.material.color = Color.red;
            Debug.Log($"{gameObject.name} turned red.");
        }
    }
}
