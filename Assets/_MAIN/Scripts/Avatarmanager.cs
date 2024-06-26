/*
Class that takes care of all the characters in the game
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] private GameObject ceoAvatar;
    [SerializeField] private GameObject managerAvatar;
    [SerializeField] private GameObject associate1Avatar;
    [SerializeField] private GameObject associate2Avatar;
    [SerializeField] private GameObject femaleAvatar;
    [SerializeField] private GameObject maleAvatar;
    [SerializeField] private GameObject info;
    [SerializeField] private GameObject task;
    [SerializeField] private GameObject feedback;
    private static bool gender;

    private Dictionary<string, GameObject> nonPlayCharacters;

    //initialising all the NPCs
    public void InitiliseAvatar()
    {
        nonPlayCharacters = new Dictionary<string, GameObject>{
            {"Dylan", ceoAvatar},
            {"Stacy",managerAvatar},
            {"Warren",associate1Avatar},
            {"Ann",associate2Avatar},
            {"Player",SelectedPlayer()},
            {"", info },
            {"Task",task },
            {"Feedback",feedback }
        };
    }

    public void SetGender(bool gender)
    {
        AvatarManager.gender = gender;
    }

    //switch players based on the choice they made
    public GameObject SelectedPlayer()
    {
        maleAvatar.SetActive(false);
        femaleAvatar.SetActive(false);
        return AvatarManager.gender ? femaleAvatar : maleAvatar;

    }


    // Activates the relevant avatar based on the name and deactivates others
    public void ActivateAvatar(string avatarName)
    {
        DeactivateAvatars();

        if (nonPlayCharacters.ContainsKey(avatarName))
        {
            nonPlayCharacters[avatarName].SetActive(true);
        }
    }

    // Deactivating avatars to ensure no overlays
    public void DeactivateAvatars()
    {
        foreach (var avatar in nonPlayCharacters.Values)
        {
            avatar.SetActive(false);
        }
    }
}
