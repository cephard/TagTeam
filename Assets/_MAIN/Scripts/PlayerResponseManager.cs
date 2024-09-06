using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResponseManager : ReadDialogue
{
    private const int FIRST_OPTION = 1;
    private const int SECOND_OPTION = 2;
    private const int THIRD_OPTION = 3;
    private const int FOURTH_OPTION = 4;
    private const int PLAYER_OPTIONS = 4;


    private ConversationUIManager conversationUIManager;
    private DialogueManager dialogueManager;

    private void Awake()
    {
        conversationUIManager = GetComponent<ConversationUIManager>();
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void InovkePlayerResponse(int currentLine)
    {
        if (String.Equals("Player", conversationUIManager.GetAvatarName()))
        {
            conversationUIManager.SwitchActiveObject();
            LoadPlayerResponses(currentLine + 1);
        }
    }

    public void LoadPlayerResponses(int startLine)
    {
        if (startLine < dialogueManager.GetLinesLength())
        {
            conversationUIManager.SetPlayerResponceOne(dialogueManager.GetLine(startLine));
            conversationUIManager.SetPlayerResponceTwo(dialogueManager.GetLine(startLine + FIRST_OPTION));
            conversationUIManager.SetPlayerResponceThree(dialogueManager.GetLine(startLine + SECOND_OPTION));
            conversationUIManager.SetPlayerResponceFour(dialogueManager.GetLine(startLine + THIRD_OPTION));
        }
        else
        {

            conversationUIManager.HidePlayerResponce();
        }
    }

    //descision between 1 to 4
    public int GetPlayerResponse(int playerChoice)
    {
        return playerChoice;
    }

    public void SavePlayerChoice(int currentLine)
    {
        PlayerReport.UpdateDecisions(dialogueManager.GetLine(currentLine));
        PlayFabDataManager.SavePlayerResponse(dialogueManager.GetLine(currentLine), currentLine);

    }

}
