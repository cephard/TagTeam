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

    private void Start()
    {
        conversationUIManager = GetComponent<ConversationUIManager>();
    }
    public void LoadPlayerResponses(int startLine, int sentenceLength)
    {
        if (startLine < sentenceLength)
        {
            conversationUIManager.SetPlayerResponceOne("#GetLine(startLine)");
            conversationUIManager.SetPlayerResponceTwo("GetLine(startLine + FIRST_OPTION)");
            conversationUIManager.SetPlayerResponceThree("GetLine(startLine + SECOND_OPTION)");
            conversationUIManager.SetPlayerResponceFour("GetLine(startLine + THIRD_OPTION)");
        }
        else
        {

            conversationUIManager.HidePlayerResponce();
        }
    }

}
