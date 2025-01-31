using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages player decisions, their corresponding outcomes, and provides feedback based on those decisions.
/// This class interacts with the CoinManager to reward or punish players for their choices.
/// </summary>
public class PlayerDecisionManager : MonoBehaviour
{
    private CoinManager coinManager;

    // Dictionary that stores player responses and the corresponding coin reward or punishment.
    private Dictionary<string, int> playerResponses = new Dictionary<string, int>()
    {
        {"I will ensure that the company is run according to the required standards.", 2},
        {"Thank you, sir. I promise I won't let you down.", 2},
        {"I will promptly brief the team and lead them through pending and upcoming tasks.", 5},
        {"With my leadership experience, I do not doubt that I'll be able to fill our manager's shoes.", 1},
        {"I promise to make you proud, by delivering my utmost best with the team.", 5},
        {"In case we are faced with any challenges I will let you know.", 2},
        {"I will always seek your guidance when I am uncertain.", 2},
        {"I think we will be fine; we have the best team for the job.", 2},
        {"Let us focus on these duties individually and for anyone lagging, my office is open for guidance and support.", 2},
        {"The CEO is relying on me so kindly leave all the major duties to me and focus on any easier duties you have so that we fast-track production.", -5},
        {"I promised our manager that we have the best team, so please ensure you find the best solutions to all issues without relying on my help.", -5},
        {"Complete the selected duties first and report any issues to me immediately. I am also expecting a report on everyone's progress by the end of the day.", 5},
        {"Yes, in the meantime, let me invite Ann over. After the break, we can work together to ensure that she completes her daily tasks.", 2},
        {"Thanks for the offer, but I need to check on Ann to see if she needs help.", 5},
        {"A cappuccino will do. Please see what is taking Ann so long.", 1},
        {"I will have it later, but first, can you assist Ann with her duties?", 2},
        {"Subcontract it to a third party: 200", 0},
        {"Replace the printer yourself.", -1},
        {"Call the maintenance guy to come over: 30", 5},
        {"[Ask Stacy for advice.]", 0},
        {"[Blame it on Stacy]", -5},
        {"[Take the fault]", 5},
        {"[Blame it on the maintenance guy]", -10},
        {"[Ignore the message]", -5},
        {"Okay, sir.", 0},
        {"I am sorry sir.", 1},
        {"This won't happen again, sir.", 2},
        {"I will do better next time sir.", 2},
        {"Tell her you took care of it and she should not worry.", 5},
        {"Tell her it's her fault that you were in trouble.", -5},
        {"It's okay I know the contract was short notice and it might have been hard to keep track of everything.", 5},
        {"[Ignore her]", -5},
        {"We still have a lot of workload at the moment, so I will give you 1 hour instead.", -5},
        {"Okay, just make sure that your daily duties are completed timely, I will ask Warren to help out, I know he won't mind.", 5},
        {"Sure, since it is your son's special day I recommend you take the day off we will handle the workload with Warren.", -3},
        {"I will allow you to go only if you are willing to compensate for those two hours by coming in 2 hours earlier tomorrow.", 0},
        {"[Report Ann to the CEO]", -5},
        {"[Ask Stacy for advice]", 1},
        {"[Call Ann into the Office]", 3},
        {"[Wait until team break and casually ask Warren about the conversation they were having]", 5},
        {"Are you trying to bribe your way out of trouble, sit down and better be quick.", -10},
        {"Let us please be professional and finish discussing this matter in the office.", 5},
        {"I just want to know why you lied to me, now take it easy and explain yourself.", -5},
        {"Thanks. Let's take a walk outside.", 5},
        {"[Tell Warren to back away and talk to his assistant]", 2},
        {"[Support Warren and condemn his assistant]", -10},
        {"[Ask Warren to explain what is going on]", 5},
        {"[Ask Warren's assistant why he is late again]", 3},
        {"[Ask Warren to compensate his assistant for the total amount]", -5},
        {"[Compensate him from your pocket]: 550", 0},
        {"[Only give him the money he is short of]: 100", 0},
        {"[Give him �100 now and ask the finance office to compensate him the entire amount]: 100", 5},
        {"[Take the deal]", -50},
        {"[Back Stacy up and let her take the Sydney office]", 50},
        {"[Let him decide on his own]", 0},
        {"[Tell him that the board's decision is final]", 0}
    };

    private ClueManager clueManager;

    /// <summary>
    /// Initializes the PlayerDecisionManager by getting references to the CoinManager and ClueManager components.
    /// </summary>
    private void Start()
    {
        coinManager = GetComponent<CoinManager>();
        clueManager = GetComponent<ClueManager>();
    }

    /// <summary>
    /// Checks the player's decision and adjusts their coin count based on the outcome.
    /// </summary>
    /// <param name="decision">The player's selected decision.</param>
    public void CheckPoorFeedBack(string decision)
    {
        if (playerResponses.ContainsKey(decision))
        {
            coinManager.AddCoins(playerResponses[decision]);
        }
        else
        {
            Debug.LogWarning("Decision not found in playerResponses.");
        }
    }

    /// <summary>
    /// Provides advice or clues based on player requests, such as asking Stacy for advice.
    /// </summary>
    /// <param name="playerRequest">The request made by the player.</param>
    /// <param name="advice">The advice to provide.</param>
    /// <param name="dialogue">The dialogue UI element to deactivate.</param>
    /// <param name="avatar">The avatar UI element to deactivate.</param>
    public void SeekAdvice(string playerRequest, string advice, GameObject dialogue, GameObject avatar)
    {
        dialogue.SetActive(false);
        avatar.SetActive(false);

        if (playerRequest == "[Ask Stacy for advice]")
        {
            clueManager.SetClue("It is best to wait until tea break and ask Ann about the issue. " +
                "Give her a chance to explain herself, and remember to be polite and friendly.");
        }

        if (playerRequest == "[Ask Stacy for advice.]")
        {
            clueManager.SetClue("You should call the maintenance guy to come over. \n" +
                "It will cost us �30 for his taxi transport but we will have saved a lot of time and resources.");
        }
    }

    /// <summary>
    /// Retrieves the player's choice, checks the feedback, and returns the choice.
    /// </summary>
    /// <param name="playerChoice">The player's decision.</param>
    /// <returns>The player's decision as a string.</returns>
    public void GetPlayerChoice(string playerChoice)
    {
        coinManager.ExtractExpenditure(playerChoice);
        CheckPoorFeedBack(playerChoice);
       // return playerChoice;
    }

    public void CapturePlayerDecision(int currentLine, string decision)
    {
        Debug.Log(decision);
        PlayerReport.UpdateDecisions(decision);
        PlayFabDataManager.SavePlayerResponse(decision, currentLine);
    }
}
