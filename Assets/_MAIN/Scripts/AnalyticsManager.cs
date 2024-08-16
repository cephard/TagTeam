using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class AnalyticsManager : MonoBehaviour
{
    // PlayFab Title ID (Set this in the Unity Editor or via script)

    // Dictionary to hold event call counts
    private Dictionary<string, int> eventCallCounts = new Dictionary<string, int>();

    private void Start()
    {


    }

    // Call this method to track an event occurrence
    public void TrackEvent(string eventName)
    {
        if (eventCallCounts.ContainsKey(eventName))
        {
            eventCallCounts[eventName]++;
        }
        else
        {
            eventCallCounts[eventName] = 1;
        }

        // Log the updated event count
        LogEventCount(eventName);

        // Optionally, send the data to PlayFab
        SendEventCountsToPlayFab(eventName);
    }

    private void LogEventCount(string eventName)
    {
        Debug.Log($"Event: {eventName}, Event Occurrence Count: {eventCallCounts[eventName]}");
    }



    // Send event counts to PlayFab
    private void SendEventCountsToPlayFab(string eventName)
    {
        foreach (var kvp in eventCallCounts)
        {
            var eventProperties = new Dictionary<string, object>
            {
                { "NameOfEvent", kvp.Key },
                //{ "EventOccurrenceCount", kvp.Value }
            };

            var request = new WriteClientPlayerEventRequest
            {
                EventName = eventName,  // Name of the event in PlayFab
                Body = eventProperties
            };

            Debug.Log($"Sending event to PlayFab: {kvp.Key}, Count: {kvp.Value}");

            PlayFabClientAPI.WritePlayerEvent(request, OnEventSent, OnEventSendError);
        }
    }

    // Callback for successful event sending
    private void OnEventSent(WriteEventResponse response)
    {
        Debug.Log("Event successfully logged");
    }

    // Callback for failed event sending
    private void OnEventSendError(PlayFabError error)
    {
        Debug.LogError($"Error logging event: {error.GenerateErrorReport()}");
    }
}
