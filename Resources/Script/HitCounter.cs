using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitCounter : MonoBehaviour
{
    public TMP_Text hitCounterText;  // Reference to the Text component on the Canvas
    private int hitCount = 0;    // Variable to track the number of hits

    void Start()
    {
        UpdateHitCounter();
    }

    public void IncrementHitCounter()
    {
        hitCount++;
        UpdateHitCounter();
    }

    private void UpdateHitCounter()
    {
        // Update the UI text with the current hit count
        hitCounterText.text = "Collecting: " + hitCount.ToString();
    }
}