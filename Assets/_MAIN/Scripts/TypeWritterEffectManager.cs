using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the typewriter effect for displaying dialogue letter by letter.
/// </summary>
public class TypeWritterEffectManager : MonoBehaviour
{
    [SerializeField]
    private float TYPING_SPEED;
    private const string EMPTY_TEXT = "";
    private Coroutine typingCoroutine;

    /// <summary>
    /// Displays the given sentence in the Text component letter by letter, with a delay between each letter.
    /// </summary>
    /// <param name="sentence">The sentence to be displayed with the typewriter effect.</param>
    /// <param name="dialogueText">The UI Text component where the sentence will be displayed.</param>
    /// <returns>IEnumerable for coroutine handling.</returns>
    public IEnumerator TypeSentence(string sentence, TextMeshProUGUI dialogue)
    {
        dialogue.text = EMPTY_TEXT;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogue.text += letter;
            yield return new WaitForSeconds(TYPING_SPEED);
        }
    }

    public void StartTypeWritter(string currentLine, TextMeshProUGUI dialogue)
    {

        typingCoroutine = StartCoroutine(TypeSentence(currentLine, dialogue));

    }

    public void StopTypeWritter()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
    }

}
