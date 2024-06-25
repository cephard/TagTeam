using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] Button taskOne;
    [SerializeField] Button taskTwo;
    [SerializeField] Button taskThree;
    [SerializeField] Button taskFour;
    [SerializeField] Button taskFive;
    [SerializeField] Button taskSix;

    private static HashSet<Button> playerTask;


    // Start is called before the first frame update
    void Start()
    {
        playerTask = new HashSet<Button>();
        playerTask.Add(taskOne);
        playerTask.Add(taskTwo);
        playerTask.Add(taskThree);
        playerTask.Add(taskFour);
        playerTask.Add(taskFive);
        playerTask.Add(taskSix);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeButtonInteractive()
    {

    }
}
