using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerRoom : MonoBehaviour
{
    [SerializeField] private GameObject Glyph;
    [SerializeField] private GameObject imageTarget;
    [SerializeField] private GameObject intro;
    public void ShowGlyph()
    {
        Glyph.SetActive(true);
        imageTarget.SetActive(false);
        intro.SetActive(false);
    }

}
