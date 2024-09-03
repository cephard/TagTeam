using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all the character avatars in the game, including NPCs and player characters.
/// </summary>
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

    /// <summary>
    /// Initializes all the non-player characters (NPCs) and the player avatar based on the selected gender.
    /// </summary>
    public void InitializeAvatar()
    {
        nonPlayCharacters = new Dictionary<string, GameObject>
        {
            { "Dylan", ceoAvatar },
            { "Stacy", managerAvatar },
            { "Warren", associate1Avatar },
            { "Ann", associate2Avatar },
            { "Player", SelectedPlayer() },
            { "", info },
            { "Task", task },
            { "Feedback", feedback }
        };
    }

    /// <summary>
    /// Sets the gender for the player character.
    /// </summary>
    /// <param name="gender">True for female, false for male.</param>
    public void SetGender(bool gender)
    {
        AvatarManager.gender = gender;
    }

    /// <summary>
    /// Selects the player avatar based on the chosen gender and deactivates the other gender avatar.
    /// </summary>
    /// <returns>The selected player avatar game object.</returns>
    public GameObject SelectedPlayer()
    {
        maleAvatar.SetActive(false);
        femaleAvatar.SetActive(false);
        return AvatarManager.gender ? femaleAvatar : maleAvatar;
    }

    /// <summary>
    /// Activates the specified avatar by name and deactivates all other avatars to avoid overlaps.
    /// </summary>
    /// <param name="avatarName">The name of the avatar to activate.</param>
    public void ActivateAvatar(string avatarName)
    {
        DeactivateAvatars();

        if (nonPlayCharacters.ContainsKey(avatarName))
        {
            nonPlayCharacters[avatarName].SetActive(true);
        }
    }

    /// <summary>
    /// Deactivates all avatars to ensure no overlays are present.
    /// </summary>
    public void DeactivateAvatars()
    {
        foreach (var avatar in nonPlayCharacters.Values)
        {
            avatar.SetActive(false);
        }
    }
}
