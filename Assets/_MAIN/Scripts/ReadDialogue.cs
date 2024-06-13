using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadDialogue : MonoBehaviour
{
    //making fields serialissable to enable access in unity
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private GameObject avatarDialogue;
    [SerializeField] private GameObject playerResponce;
    [SerializeField] private GameObject ceoAvatar;
    [SerializeField] private GameObject managerAvatar;
    [SerializeField] private GameObject associate1Avatar;
    [SerializeField] private GameObject associate2Avatar;
    private int currentLine = 0;
    private string[] lines;
    private Dictionary<string, GameObject> nonPlayCharacters;

    //initialising all the NPCs
    private void InitiliseAvatar()
    {
        nonPlayCharacters = new Dictionary<string, GameObject>{
            {"Dylan", ceoAvatar},
            {"Stacy",managerAvatar},
            {"Warren",associate1Avatar},
            {"Ann",associate2Avatar}
        };
    }
    /*loading the text file with the dialogues and setting the eponsences to wait for the NPC dialogues
    skipping blank lines to ensure seamless conversation
    */
    private void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("General");
        playerResponce.SetActive(false);
        InitiliseAvatar();
        DeactivateAvatars();
        if (textAsset != null)
        {
            lines = textAsset.text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            SetNameAndDialogue(currentLine);

        }
        else
        {
            //in case the file responce is not found this text will be displayed insted
            dialogue.text = "Oops! Sorry I'll get back to you soon I have an urgent meeting!";
        }
    }

    //returns the line on the specifi index
    private string GetLineIndex(int lineIndex)
    {
        if (lines != null && lineIndex < lines.Length)
        {
            return lines[lineIndex];
        }
        else
        {
            return "No Lines To Read";
        }
    }

    //reading the next line and dsabling the parent game object if there are no lines to read.
    public void NextLine()
    {
        if (lines != null && currentLine < lines.Length - 1)
        {
            currentLine++;
            SetNameAndDialogue(currentLine);

        }
        else
        {
            avatarDialogue.SetActive(false);
            playerResponce.SetActive(true);
        }
    }

    // Sets the dialogue and avatar name based on the current line index
    private void SetNameAndDialogue(int lineIndex)
    {
        if (lines != null && lineIndex < lines.Length)
        {
            string line = lines[lineIndex];
            string[] parts = line.Split(new[] { ':' }, 2);

            if (parts.Length == 2)
            {
                avatarName.text = parts[0].Trim();
                dialogue.text = parts[1].Trim();
                ActivateAvatar(avatarName.text);
            }
            else
            {
                dialogue.text = line;
            }
        }
        else
        {
            avatarName.text = "Unknown";
            dialogue.text = "No Lines To Read";
        }
    }

    // Activates the relevant avatar based on the name and deactivates others
    private void ActivateAvatar(string avatarName)
    {
        DeactivateAvatars();

        if(nonPlayCharacters.ContainsKey(avatarName)){
            nonPlayCharacters[avatarName].SetActive(true);
        }

    }

    // Deactivating avatars to ensure no overlays
    private void DeactivateAvatars()
    {
        foreach(var avatar in nonPlayCharacters.Values){
            avatar.SetActive(false);
        }
    }
}
