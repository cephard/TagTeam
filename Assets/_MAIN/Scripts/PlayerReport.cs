using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReport : MonoBehaviour
{
    [SerializeField] private Text report;
    private static string reportText = "";
    private static int choiseNumber;

    public void Start()
    {
        report.text = reportText;
    }

    public static void UpdateDecisions(string playerChoise)
    {
        choiseNumber++;
        reportText += (choiseNumber.ToString() + " : " + playerChoise + "\n");
    }
}

