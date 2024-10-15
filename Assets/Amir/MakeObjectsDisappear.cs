using UnityEngine;

public class MakeObjectsDisappear : MonoBehaviour
{
    public GameObject object1; // Assign the first object to hide in the inspector
    public GameObject object2; // Assign the second object to hide in the inspector

    // Method to hide the objects
    public void HideSpecifiedObjects()
    {
        if (object1 != null && object1.activeSelf)
        {
            object1.SetActive(false);
            Debug.Log("Object 1 has been hidden.");
        }

        if (object2 != null && object2.activeSelf)
        {
            object2.SetActive(false);
            Debug.Log("Object 2 has been hidden.");
        }
    }

    // Example trigger: Hide the objects when this GameObject is clicked
    private void OnMouseDown()
    {
        Debug.Log("Hiding objects 1 and 2...");
        HideSpecifiedObjects();
    }
}
