using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Text welcome;

    void Start()
    {
        welcome.text = "Welcome " + PlayerPrefs.GetString("Username");
    }
}
