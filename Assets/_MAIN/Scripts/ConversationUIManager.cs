using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConversationUIManager : ReadDialogue
{

    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI avatarName;
    [SerializeField] private GameObject avatarDialogue;
    [SerializeField] private GameObject playerResponse;
    [SerializeField] private Text playerReponseOne;
    [SerializeField] private Text playerReponseTwo;
    [SerializeField] private Text playerReponseThree;
    [SerializeField] private Text playerReponseFour;


    private ClueManager clueManager;

    public void Awake()
    {
        HidePlayerResponce();
    }

    private void Start()
    {
        clueManager = GetComponent<ClueManager>();
        clueManager.OverRideClueOnStart(playerResponse);
    }

    public bool IsPlayerSpeaking()
    {
        return String.Equals("Player", avatarName.text);
    }

    public void HidePlayerResponce()
    {
        playerResponse.SetActive(false);
    }

    public void SetDialogueText(string newDialogue)
    {
        dialogue.text = newDialogue;
    }

    public void SetAvatarName(string newAvatarName)
    {
        avatarName.text = newAvatarName;
    }

    public void SwitchActiveObject()
    {
        //GameObject avatarDialogue, GameObject playerResponse
        bool isAvatarDialogueActive = avatarDialogue.activeSelf;
        avatarDialogue.SetActive(!isAvatarDialogueActive);
        playerResponse.SetActive(isAvatarDialogueActive);
    }

    public void SetPlayerResponceOne(string newResponse)
    {
        playerReponseOne.text = newResponse;
    }

    public void SetPlayerResponceTwo(string newResponse)
    {
        playerReponseTwo.text = newResponse;
    }

    public void SetPlayerResponceThree(string newResponse)
    {
        playerReponseThree.text = newResponse;
    }

    public void SetPlayerResponceFour(string newResponse)
    {
        playerReponseFour.text = newResponse;
    }

    public GameObject GetAvatarDialogue()
    {
        return avatarDialogue;
    }

    public GameObject GetPlayerResponse()
    {
        return playerResponse;
    }

    public string GetAvatarName()
    {
        return avatarName.text;
    }

    public string GetDialogueText()
    {
        return dialogue.text;
    }

    public TextMeshProUGUI GetDialogue()
    {
        return dialogue;
    }

    public void ReportEmptyStoryLine(TextAsset storyLine)
    {

        if (storyLine == null)
        {
            SetDialogueText("Oops! It seems like you the is no storyline!");
            return;
        }
    }




}
