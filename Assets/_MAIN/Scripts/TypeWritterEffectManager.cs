using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeWritterEffectManager : ReadDialogue
{
    public IEnumerator TypeWritterEffect(string sentence, string dialogueText)
    {
        dialogueText = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
//dialogue.text