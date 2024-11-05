using UnityEngine;

public class PitColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player collided with: {gameObject.name}");

            // Check for main objects
            if (gameObject.CompareTag("Object3") || 
                gameObject.CompareTag("Object4") || 
                gameObject.CompareTag("Object5") || 
                gameObject.CompareTag("Object6"))
            {
                PitManager.Instance.OnObjectCollision(gameObject);  
            }
            else if (gameObject.CompareTag("EndZone"))
            {
                PitManager.Instance.OnReachEndZone();  
            }
            // Check for external objects and reset if collided
            else if (gameObject.CompareTag("ExternalObject"))
            {
                Debug.Log("Player collided with an external object. Playing sound and resetting everything.");
                PitManager.Instance.OnExternalObjectCollision();
            }
        }
    }
}
