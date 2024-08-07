using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System;
using PlayFab;
using PlayFab.ClientModels;
using System.Threading.Tasks;


public class LeaderBoardManager : UnityEngine.MonoBehaviour
{
    private static bool isGettingUserData;
    private static Dictionary<string, UserDataRecord> localUserData;

    public static void SaveData(Dictionary<string, string> onlineUserData,
        Action<UpdateUserDataResult> onSucces, Action<PlayFabError> onFail)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = onlineUserData
        },
        successResult =>
        {
            //local copy
            if (onlineUserData != null)
            {
                foreach (var key in localUserData.Keys)
                {
                    UserDataRecord value = new()
                    {
                        Value = onlineUserData[key]
                    };

                    if (localUserData.ContainsKey(key)) localUserData[key] = value;
                    else localUserData.Add(key, value);
                }
                onSucces(successResult);
            }
        }, onFail);
    }

    public static void GetUserData(
        Action<GetUserDataResult> onSuccess,
        Action<PlayFabError> onFail)
    {

        DelayUserData();
        if (localUserData != null)
        {
            onSuccess(new GetUserDataResult() { Data = localUserData });
            return;
        }

        isGettingUserData = true;
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(),
            onSuccessResult =>
            {
                localUserData = onSuccessResult.Data;
                isGettingUserData = false;
                onSuccess(onSuccessResult);
            },

            onFailResult =>
            {
                isGettingUserData = true;
                onFail(onFailResult);
            });
    }

    private static void DelayUserData()
    {
        while (isGettingUserData)
        {
            Task.Delay(100);
        }

    }
}
