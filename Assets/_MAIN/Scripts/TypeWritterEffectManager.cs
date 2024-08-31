using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypeWritterEffectManager : ReadDialogue
{
    private const float TYPING_SPEED = 0.05f;

    public IEnumerator TypeWritterEffect(string sentence, Text dialogueText)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(TYPING_SPEED);
        }
    }
}
