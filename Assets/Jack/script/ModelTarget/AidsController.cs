using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AidsController : MonoBehaviour
{
    public GameObject textBoxPrefab; // refab
    public Transform canvasTransform; // the canvas that shows the textbox
    public string sceneId; // id of the current scene
    public string eventId="A1"; // id of the current scene
    public AudioClip[] VoiceAids; // array of voice aids
    public AudioSource audioSource; // audio player
    private GameObject textBox;
    private void Start()
    {
        PlayerPrefs.SetString("SceneAidCode", sceneId);
        PlayerPrefs.SetString("EventAidCode", eventId);
        PlayerPrefs.Save();
        // use scipt to bind event
        //ModelTargetBehaviour modelTarget = GetComponent<ModelTargetBehaviour>();
        //if (modelTarget)
        //
            //modelTarget.OnTargetStatusChanged += OnTargetFound;
        //}
    }

    public void AidsOnTargetFound()
    {
       string sceneEventAidCode = PlayerPrefs.GetString("SceneAidCode", "S0")+ PlayerPrefs.GetString("EventAidCode", "A1");
       AidsEvent(sceneEventAidCode);
    }
    public void AidsOnTargetLost()
    {
        textBox.SetActive(false);
        audioSource.Stop();
    }
    public void PlayAudioBasedOnSceneEventId(string content)
    {
        if (string.IsNullOrEmpty(content) || content.Length < 4)
        {
            Debug.LogError("Invalid content format");
            return;
        }

        char aidChar = content[content.Length - 1];
        int aidIndex;
        if (int.TryParse(aidChar.ToString(), out aidIndex))
        {
            aidIndex -= 1;
            if (aidIndex >= 0 && aidIndex < VoiceAids.Length)
            {
                // 播放对应的音频
                audioSource.clip = VoiceAids[aidIndex];
                audioSource.Play();
                Debug.Log($"Playing audio for Aid: {aidIndex + 1}");
            }
            else
            {
                Debug.LogError($"Aid index {aidIndex + 1} is out of range.");
            }
        }
        else
        {
            Debug.LogError("Failed to parse Aid index from content.");
        }
    }
    private void AidsEvent(string content)
    {
        PlayAudioBasedOnSceneEventId(content);
        textBox = Instantiate(textBoxPrefab, canvasTransform);

        // text content
        TMPro.TextMeshProUGUI textComponent = textBox.GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if (textComponent)
        {
            string displayText = content switch
            {
                "S0A1" => "Find the crime scene tape on the ground, stand inside it, when inside, find the lens image and point your camera at it to collect the lens.",
                "S1A1" => "Look around the classroom with your lens on to find a projector. Turn on the projector to see the message that appears on the board.",
                "S1A2" => "Read the information on the board and proceed to the hallway.",
                "S2A1" => "Locate professor Kyle’s portrait and scan it for further instructions.",
                "S2A2" => "Navigate through the maze. You need to find the correct path by trial and error. The maze times out after some time. You can scan the professor’s portrait to make the maze appear again.",
                "S2A3" => "Locate the hourglass and tab on it to activate it.",
                "S4A1" => "You are traveling back in time, you have to navigate your way through the past. If you make a mistake, you will be lost in oblivion. Cross the oblivion by taping in front of yourself and placing a block. Gradually cross the oblivion by placing more blocks. ",
                "S4A2" => "Navigate to the classroom to find Professor Kyle’s ghost. Tap on the ghost figure to talk to him. Make sure you are close enough to hear him.",
                "S4A3" => "Find the box that Dr. Kyle hid in the classroom. Inside, you can find clues about who murdered him.",
                "S4A4" => "Locate the hourglass and tab on it to activate it.",
                "S5A1" => "You are traveling back in time, you have to navigate your way through the past. If you make a mistake, you will be lost in oblivion. Cross the oblivion by taping in front of yourself and placing a block. Gradually cross the oblivion by placing more blocks.",
                "S5A2" => "Go to the locker room in the hallway. Find Dr. Kyle’s locker. It is locked. You should solve a glyph to open the locker. ",
                "S5A3" => "You opened the locker. Look inside and tap on the item inside the locker.",
                "S5A4" => "Locate the hourglass and tab on it to activate it.",
                "S6A1" => "You are traveling back in time, you have to navigate your way through the past. If you make a mistake, you will be lost in oblivion. Cross the oblivion by taping in front of yourself and placing a block. Gradually cross the oblivion by placing more blocks. ",
                "S6A2" => "Go to the other hallway and look for an envelope with the game logo on it.",
                "S6A3" => "Locate the hourglass and tab on it to activate it.",
                "S7A1" => "You are traveling back in time, you have to navigate your way through the past. If you make a mistake, you will be lost in oblivion. Cross the oblivion by taping in front of yourself and placing a block. Gradually cross the oblivion by placing more blocks. \r\n",
                "S7A2" => "Find and Talk to Dr. Kyle’s ghost to end the game.",
                _ => "Unknown Code",
            };

            // 设置文本框内容
            textComponent.text = displayText;
        }
    }
}
