using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignmentOneLogic : MonoBehaviour
{
    [SerializeField] private Image SlotOne;
    [SerializeField] private Image SlotTwo;
    [SerializeField] private Image SlotThree;
    [SerializeField] private Image SlotFour;
    [SerializeField] private Image SlotFive;
    [SerializeField] private Image SlotSix;
    [SerializeField] private Image SlotSeven;

    [SerializeField] private Image TaskOne;
    [SerializeField] private Image TaskTwo;
    [SerializeField] private Image TaskThree;
    [SerializeField] private Image TaskFour;
    [SerializeField] private Image TaskFive;
    [SerializeField] private Image TaskSix;
    [SerializeField] private Image TaskSeven;

    private int counter;

    void Start()
    {
        counter = 0;
        CompareTexts();
    }

    void CompareTexts()
    {
        // Access the Text components of Slot images
        Text slotTextOne = SlotOne.GetComponentInChildren<Text>();
        Text slotTextTwo = SlotTwo.GetComponentInChildren<Text>();
        Text slotTextThree = SlotThree.GetComponentInChildren<Text>();
        Text slotTextFour = SlotFour.GetComponentInChildren<Text>();
        Text slotTextFive = SlotFive.GetComponentInChildren<Text>();
        Text slotTextSix = SlotSix.GetComponentInChildren<Text>();
        Text slotTextSeven = SlotSeven.GetComponentInChildren<Text>();

        // Access the Text components of Task images
        Text taskTextOne = TaskOne.GetComponentInChildren<Text>();
        Text taskTextTwo = TaskTwo.GetComponentInChildren<Text>();
        Text taskTextThree = TaskThree.GetComponentInChildren<Text>();
        Text taskTextFour = TaskFour.GetComponentInChildren<Text>();
        Text taskTextFive = TaskFive.GetComponentInChildren<Text>();
        Text taskTextSix = TaskSix.GetComponentInChildren<Text>();
        Text taskTextSeven = TaskSeven.GetComponentInChildren<Text>();

        // Compare the texts and update the counter
        if (slotTextOne.text == taskTextOne.text) counter++;
        if (slotTextTwo.text == taskTextTwo.text) counter++;
        if (slotTextThree.text == taskTextThree.text) counter++;
        if (slotTextFour.text == taskTextFour.text) counter++;
        if (slotTextFive.text == taskTextFive.text) counter++;
        if (slotTextSix.text == taskTextSix.text) counter++;
        if (slotTextSeven.text == taskTextSeven.text) counter++;

        Debug.Log("Matching texts count: " + counter);
    }
}
