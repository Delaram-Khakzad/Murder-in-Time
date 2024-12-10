using System.Collections;
using UnityEngine;

public class End : MonoBehaviour
{
    public GameObject winPanel;

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f); // Add a 1-second delay (or adjust as needed)
        winPanel.SetActive(true);
    }
}
