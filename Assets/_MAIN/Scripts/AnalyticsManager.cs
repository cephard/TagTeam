using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

/// <summary>
/// Manages the tracking and reporting of events using PlayFab analytics.
/// </summary>
public class AnalyticsManager : MonoBehaviour
{
    private const int ONE_EVENT = 1;
    private Dictionary<string, int> eventCallCounts = new Dictionary<string, int>();

    /// <summary>
    /// Tracks an event occurrence by incrementing the event count and sending the data to PlayFab.
    /// </summary>
    /// <param name="eventName">The name of the event to track.</param>
    public void TrackEvent(string eventName)
    {
        if (eventCallCounts.ContainsKey(eventName))
        {
            eventCallCounts[eventName]++;
        }
        else
        {
            eventCallCounts[eventName] = ONE_EVENT;
        }

        // Log the updated event count
        LogEventCount(eventName);

        // Optionally, send the data to PlayFab
        SendEventCountsToPlayFab(eventName);
    }

    /// <summary>
    /// Logs the current count of the specified event to the Unity console.
    /// </summary>
    /// <param name="eventName">The name of the event whose count is to be logged.</param>
    private void LogEventCount(string eventName)
    {
        Debug.Log($"Event: {eventName}, Event Occurrence Count: {eventCallCounts[eventName]}");
    }

    /// <summary>
    /// Sends the event counts to PlayFab.
    /// </summary>
    /// <param name="eventName">The name of the event being reported to PlayFab.</param>
    private void SendEventCountsToPlayFab(string eventName)
    {
        foreach (var kvp in eventCallCounts)
        {
            var eventProperties = new Dictionary<string, object>
            {
                { "NameOfEvent", kvp.Key },
                { "EventOccurrenceCount", kvp.Value }
            };

            var request = new WriteClientPlayerEventRequest
            {
                EventName = eventName,
                Body = eventProperties
            };

            Debug.Log($"Sending event to PlayFab: {kvp.Key}, Count: {kvp.Value}");

            PlayFabClientAPI.WritePlayerEvent(request, OnEventSent, OnEventSendError);
        }
    }

    /// <summary>
    /// Callback invoked when an event is successfully sent to PlayFab.
    /// </summary>
    /// <param name="response">The response received from PlayFab.</param>
    private void OnEventSent(WriteEventResponse response)
    {
        Debug.Log("Event successfully logged");
    }

    /// <summary>
    /// Callback invoked when there is an error sending an event to PlayFab.
    /// </summary>
    /// <param name="error">The error information received from PlayFab.</param>
    private void OnEventSendError(PlayFabError error)
    {
        Debug.LogError($"Error logging event: {error.GenerateErrorReport()}");
    }
}
