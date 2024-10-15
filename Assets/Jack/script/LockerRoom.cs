using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerRoom : MonoBehaviour
{
    [SerializeField] private GameObject Glyph;
    [SerializeField] private GameObject imageTarget;
    public void ShowGlyph()
    {
        Glyph.SetActive(true);
        imageTarget.SetActive(false);
    }

}
