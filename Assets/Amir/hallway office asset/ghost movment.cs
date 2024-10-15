using UnityEngine;

public class SlowMove : MonoBehaviour
{
    // Speed of movement (adjustable from the Inspector)
    [SerializeField] private float speed = 0.1f;

    void Update()
    {
        // Move the object slowly in the positive X direction
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
