using UnityEngine;

public class PitColliderHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Player collided with: {gameObject.name}");

            if (gameObject.CompareTag("Object3"))
            {
                PitManager.Instance.OnObjectCollision(gameObject);  
            }
            else if (gameObject.CompareTag("Object4"))
            {
                PitManager.Instance.OnObjectCollision(gameObject);  
            }
            else if (gameObject.CompareTag("EndZone"))
            {
                PitManager.Instance.OnReachEndZone();  
            }
        }
    }
}
