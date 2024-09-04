using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

/// <summary>
/// Manages saving and retrieving user data from PlayFab with caching for local data access.
/// </summary>
public class LeaderBoardManager : MonoBehaviour
{
    private static Dictionary<string, UserDataRecord> localUserData;
    private static bool isFetchingData = false;

    /// <summary>
    /// Saves user data to PlayFab and updates the local cache.
    /// </summary>
    /// <param name="onlineUserData">A dictionary of the data to be saved online.</param>
    /// <param name="onSuccess">Callback for when the data is successfully saved.</param>
    /// <param name="onFailure">Callback for when saving fails.</param>
    public static void SaveData(Dictionary<string, string> onlineUserData, Action<UpdateUserDataResult> onSuccess, Action<PlayFabError> onFailure)
    {
        if (onlineUserData == null || onlineUserData.Count == 0)
        {
            onFailure?.Invoke(new PlayFabError { ErrorMessage = "No data to save." });
            return;
        }

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest { Data = onlineUserData },
        result =>
        {
            UpdateLocalCache(onlineUserData); // Update the local cache after saving
            onSuccess?.Invoke(result);
        },
        error =>
        {
            onFailure?.Invoke(error);
        });
    }

    /// <summary>
    /// Retrieves user data from PlayFab. Uses the local cache if available.
    /// </summary>
    /// <param name="onSuccess">Callback for when data retrieval is successful.</param>
    /// <param name="onFailure">Callback for when data retrieval fails.</param>
    public static void GetUserData(Action<GetUserDataResult> onSuccess, Action<PlayFabError> onFailure)
    {
        if (localUserData != null)
        {
            onSuccess?.Invoke(new GetUserDataResult { Data = localUserData });
            return;
        }

        if (isFetchingData) // Prevent multiple simultaneous fetches
        {
            Task.Delay(100).ContinueWith(t => GetUserData(onSuccess, onFailure));
            return;
        }

        isFetchingData = true;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
        result =>
        {
            localUserData = result.Data;
            isFetchingData = false;
            onSuccess?.Invoke(result);
        },
        error =>
        {
            isFetchingData = false;
            onFailure?.Invoke(error);
        });
    }

    /// <summary>
    /// Updates the local cache with the latest online data.
    /// </summary>
    /// <param name="onlineUserData">The data to update in the local cache.</param>
    private static void UpdateLocalCache(Dictionary<string, string> onlineUserData)
    {
        if (localUserData == null)
        {
            localUserData = new Dictionary<string, UserDataRecord>();
        }

        foreach (var key in onlineUserData.Keys)
        {
            if (localUserData.ContainsKey(key))
            {
                localUserData[key].Value = onlineUserData[key];
            }
            else
            {
                localUserData[key] = new UserDataRecord { Value = onlineUserData[key] };
            }
        }
    }
}
