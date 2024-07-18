using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Threading.Tasks;

public static class PlayFabDataManager
{
    static Dictionary<string, UserDataRecord> userData;
    static bool isGettingUserData = false;

    public static void SaveData(Dictionary<string, string> data,
        Action<UpdateUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = data
        },
        SuccessResult =>
        {
            //Updating local copy of user data
            if (userData != null)

                foreach (var key in data.Keys)
                {
                    UserDataRecord Value = new() { Value = data[key] };

                    if (userData.ContainsKey(key)) userData[key] = Value;
                    else userData.Add(key, Value);
                }

            onSuccess(SuccessResult);
        },
        onFail);
    }

    public static void GetUserData(
        Action<GetUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {
        while (isGettingUserData)
        {
            Task.Delay(100);
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
            }, onFailResult =>
            {
                isGettingUserData = false;
                onFail(onFailResult);
            });
    }
}
