using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    [SerializeField] private Text clueText;
    [SerializeField] private GameObject clueDisplay;

    public void ShowClue()
    {
        clueDisplay.SetActive(true);
    }
    public void HideClue()
    {
        clueDisplay.SetActive(false);
    }

    public void ShowWinOrLooseClue(string clueName)
    {
        clueDisplay.SetActive(true);
        clueText.text = clueName;
    }
}
