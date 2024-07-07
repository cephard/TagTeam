using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLaptopManager : MonoBehaviour
{
    [SerializeField] private Text clueText;
    [SerializeField] private GameObject clueDisplay;

    public void UpdateClue(string clue)
    {
        if (clueText == null)
        {

        }
        clueDisplay.SetActive(true);
        clueText.text = clue;
    }

    public void HideClue()
    {
        clueDisplay.SetActive(false);
    }
}
