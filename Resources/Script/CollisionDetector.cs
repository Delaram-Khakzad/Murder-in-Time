using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    private HitCounter hitCounter;
    void Start()
    {
        // Find the HitCounter script in the scene (assumes only one instance)
        hitCounter = FindObjectOfType<HitCounter>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject.CompareTag("Player"))
        {
            // Notify the hit counter
            hitCounter.IncrementHitCounter();

            // Destroy the object after it hits the target (optional)
            Destroy(gameObject);
        }
    }
}
