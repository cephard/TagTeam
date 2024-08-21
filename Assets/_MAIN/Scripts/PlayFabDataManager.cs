using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class PlayFabDataManager
{
    static Dictionary<string, UserDataRecord> userData;
    static bool isGettingUserData = false;

    private const int MaxDataSize = 1024; // Adjust this size based on your needs

    public static void SaveLongResponse(string key, string longResponse,
        Action<UpdateUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        int chunkCount = (longResponse.Length + MaxDataSize - 1) / MaxDataSize;

        for (int i = 0; i < chunkCount; i++)
        {
            string chunkKey = $"{key}_part_{i}";
            string chunkValue = longResponse.Substring(i * MaxDataSize, Math.Min(MaxDataSize, longResponse.Length - i * MaxDataSize));
            data[chunkKey] = chunkValue;
        }

        SaveData(data, onSuccess, onFail);
    }

    public static void GetLongResponse(string key,
        Action<string> onSuccess,
        Action<PlayFabError> onFail)
    {
        GetUserData(
            onSuccessResult =>
            {
                StringBuilder responseBuilder = new StringBuilder();
                int partIndex = 0;

                while (true)
                {
                    string chunkKey = $"{key}_part_{partIndex}";
                    if (!onSuccessResult.Data.ContainsKey(chunkKey))
                        break;

                    responseBuilder.Append(onSuccessResult.Data[chunkKey].Value);
                    partIndex++;
                }

                onSuccess(responseBuilder.ToString());
            },
            onFail);
    }

    public static void SaveData(Dictionary<string, string> data,
        Action<UpdateUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = data
        },
        successResult =>
        {
            if (userData != null)
            {
                foreach (var key in data.Keys)
                {
                    UserDataRecord value = new() { Value = data[key] };

                    if (userData.ContainsKey(key))
                        userData[key] = value;
                    else
                        userData.Add(key, value);
                }
            }

            onSuccess(successResult);
        },
        onFail);
    }

    public static void GetUserData(
        Action<GetUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {
        if (isGettingUserData)
        {
            Task.Delay(100).ContinueWith(t => GetUserData(onSuccess, onFail)); // Avoid blocking main thread
            return;
        }

        if (userData != null)
        {
            onSuccess(new GetUserDataResult() { Data = userData });
            return;
        }

        isGettingUserData = true;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            onSuccessResult =>
            {
                userData = onSuccessResult.Data;
                isGettingUserData = false;
                onSuccess(onSuccessResult);
            },
            onFailResult =>
            {
                isGettingUserData = false;
                onFail(onFailResult);
            });
    }

    public static void SavePlayerResponse(string response, int currentLine)
    {
        // Save the player response to PlayFab using a unique key
        string responseKey = $"PlayerResponse_{currentLine}"; // Ensure this key is unique per response
        SaveLongResponse(responseKey, response,
            successResult => Debug.Log("Player response saved successfully."),
            error => Debug.LogError($"Error saving player response: {error.GenerateErrorReport()}"));
    }
}
