using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Manages PlayFab data operations, including saving and retrieving player responses across multiple key-value pairs.
/// </summary>
public static class PlayFabDataManager
{
    static Dictionary<string, UserDataRecord> userData;
    static bool isGettingUserData = false;

    private const int MaxDataSize = 1024;

    /// <summary>
    /// Saves a long response by splitting it into multiple chunks and storing each part under a unique key in PlayFab.
    /// </summary>
    /// <param name="key">The base key for storing the response chunks.</param>
    /// <param name="longResponse">The long response string to save.</param>
    /// <param name="onSuccess">Callback for success, invoked with the result of the update.</param>
    /// <param name="onFail">Callback for failure, invoked with the PlayFab error.</param>
    public static void SaveLongResponse(string key, string longResponse,
        Action<UpdateUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        CreateResponseChunks(key, longResponse, data);
        SaveData(data, onSuccess, onFail);
    }

    // Loop through the long response and break it into chunks of size MaxDataSize.
    private static void CreateResponseChunks(string key, string longResponse, Dictionary<string, string> data)
    {
        int chunkCount = (longResponse.Length + MaxDataSize - 1) / MaxDataSize;

        for (int i = 0; i < chunkCount; i++)
        {
            string chunkKey = $"{key}_part_{i}";
            string chunkValue = longResponse.Substring(i * MaxDataSize, Math.Min(MaxDataSize, longResponse.Length - i * MaxDataSize));
            data[chunkKey] = chunkValue;
        }
    }

    /// <summary>
    /// Retrieves a long response by fetching all the chunks stored with a base key from PlayFab and reconstructing the string.
    /// </summary>
    /// <param name="key">The base key for retrieving the response chunks.</param>
    /// <param name="onSuccess">Callback for success, invoked with the reconstructed response.</param>
    /// <param name="onFail">Callback for failure, invoked with the PlayFab error.</param>
    public static void GetLongResponse(string key,
            Action<string> onSuccess,
            Action<PlayFabError> onFail)
    {
        GetUserData(
            onSuccessResult =>
            {
                string reconstructedResponse = ReconstructResponse(onSuccessResult.Data, key);
                onSuccess(reconstructedResponse);
            },
            onFail);
    }

    /// <summary>
    /// Reconstructs a long response by fetching chunks stored in the data dictionary.
    /// </summary>
    /// <param name="data">The dictionary containing the chunked data.</param>
    /// <param name="key">The base key for retrieving the response chunks.</param>
    /// <returns>The fully reconstructed response string.</returns>
    private static string ReconstructResponse(Dictionary<string, UserDataRecord> data, string key)
    {
        StringBuilder responseBuilder = new StringBuilder();
        int partIndex = 0;

        // Loop over the chunked data and reconstruct the full response string.
        // Continues appending until it no longer finds a valid chunk key.
        while (true)
        {
            string chunkKey = $"{key}_part_{partIndex}";
            if (!data.ContainsKey(chunkKey))  // Stop if no chunk with this part index exists
                break;

            responseBuilder.Append(data[chunkKey].Value);
            partIndex++;
        }

        return responseBuilder.ToString();
    }

    /// <summary>
    /// Saves a dictionary of data to PlayFab.
    /// </summary>
    /// <param name="data">A dictionary containing the data to save, with keys and values.</param>
    /// <param name="onSuccess">Callback for success, invoked with the result of the update.</param>
    /// <param name="onFail">Callback for failure, invoked with the PlayFab error.</param>
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
            // Update the local cache if userData exists
            if (userData != null)
            {
                UpdateLocalCopy(data, userData);
            }

            onSuccess(successResult);
        },
        onFail);
    }

    /// <summary>
    /// Updates the local copy of user data with the new data.
    /// </summary>
    /// <param name="data">The dictionary containing the new data to be saved locally.</param>
    /// <param name="userData">The local cache of user data to be updated.</param>
    private static void UpdateLocalCopy(Dictionary<string, string> data, Dictionary<string, UserDataRecord> userData)
    {
        // Loop through the saved data and update the local userData cache.
        foreach (var key in data.Keys)
        {
            UserDataRecord value = new() { Value = data[key] };

            // Either update an existing record or add a new one to the cache.
            if (userData.ContainsKey(key))
                userData[key] = value;
            else
                userData.Add(key, value);
        }
    }

    /// <summary>
    /// Retrieves user data from PlayFab and caches it. If already fetching, it waits and retries.
    /// </summary>
    /// <param name="onSuccess">Callback for success, invoked with the retrieved user data.</param>
    /// <param name="onFail">Callback for failure, invoked with the PlayFab error.</param>
    public static void GetUserData(
        Action<GetUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {
        if (isGettingUserData)
        {
            // If already fetching, delay and retry to avoid blocking.
            Task.Delay(100).ContinueWith(t => GetUserData(onSuccess, onFail));
            return;
        }

        if (userData != null)
        {
            onSuccess(new GetUserDataResult() { Data = userData });  // Return cached data if available
            return;
        }

        isGettingUserData = true;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            onSuccessResult =>
            {
                userData = onSuccessResult.Data;  // Cache the retrieved data
                isGettingUserData = false;
                onSuccess(onSuccessResult);
            },
            onFailResult =>
            {
                isGettingUserData = false;
                onFail(onFailResult);
            });
    }

    /// <summary>
    /// Saves a player's response to PlayFab using a unique key for each response.
    /// </summary>
    /// <param name="response">The player's response to be saved.</param>
    /// <param name="currentLine">The current line or index to ensure a unique key.</param>
    public static void SavePlayerResponse(string response, int currentLine)
    {
        string responseKey = $"PlayerResponse_{currentLine}";
        SaveLongResponse(responseKey, response,
            successResult => Debug.Log("Player response saved successfully."),
            error => Debug.LogError($"Error saving player response: {error.GenerateErrorReport()}"));
    }
}
