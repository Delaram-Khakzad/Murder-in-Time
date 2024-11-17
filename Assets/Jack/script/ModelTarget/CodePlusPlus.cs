using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePlusPlus : MonoBehaviour
{
    // Start is called before the first frame update
    public void eventCodePlusPlus()
    {
        string eId = PlayerPrefs.GetString("EventAidCode", "A1");
        char aidChar = eId[eId.Length - 1];
        int aidIndex;
        int.TryParse(aidChar.ToString(), out aidIndex);
        aidIndex++;
        PlayerPrefs.SetString("EventAidCode", "A" + aidIndex);
        Debug.Log(PlayerPrefs.GetString("SceneAidCode", "S0") + PlayerPrefs.GetString("EventAidCode", "A1"));
    } 
}
