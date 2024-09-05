using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResponceManager : ReadDialogue
{
    private const int FIRST_OPTION = 1;
    private const int SECOND_OPTION = 2;
    private const int THIRD_OPTION = 3;
    private const int FOURTH_OPTION = 4;
    private const int PLAYER_OPTIONS = 4;

    [SerializeField] private GameObject playerResponse;
    [SerializeField] private Text playerReponseOne;
    [SerializeField] private Text playerReponseTwo;
    [SerializeField] private Text playerReponseThree;
    [SerializeField] private Text playerReponseFour;

    public void LoadPlayerResponses(int startLine, int sentenceLength)
    {

        //lines.length = sentencelength
        //if (startLine < lines.Length)
        if (startLine < sentenceLength)
        {
            playerReponseOne.text = GetLine(startLine);
            playerReponseTwo.text = GetLine(startLine + FIRST_OPTION);
            playerReponseThree.text = GetLine(startLine + SECOND_OPTION);
            playerReponseFour.text = GetLine(startLine + THIRD_OPTION);
        }
        else
        {
            playerResponse.SetActive(false);
        }
    }


}
