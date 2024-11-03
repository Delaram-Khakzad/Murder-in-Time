using UnityEngine;

public class DisappearOnChildTrigger : MonoBehaviour
{
    public void DisappearParent()
    {
        gameObject.SetActive(false); // Make the parent object disappear
    }
}
