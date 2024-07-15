using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TradeGuideUI : MonoBehaviour
{
    [SerializeField] private Text dialogueText;
    [SerializeField] private Button Dialogue;
    private Queue<string> dialogues;
    private bool isDialogueSkipping = false;

    void Start()
    {
        dialogues = new Queue<string>();
        skipButton.gameObject.SetActive(false);
    }

    public void StartDialogue(List<string> newDialogues)
    {
        dialogues.Clear();
        foreach (string dialogue in newDialogues)
        {
            dialogues.Enqueue(dialogue);
        }
        DisplayNextDialogue();
    }

    public void DisplayNextDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }
        string dialogue = dialogues.Dequeue();
        dialogueText.text = "";
        StartCoroutine(TypeSentence(dialogue));
    }

    IEnumerator TypeSentence(string sentence)
    {
        skipButton.gameObject.SetActive(true);
        isDialogueSkipping = false;
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            if (isDialogueSkipping) yield break;
            yield return new WaitForSeconds(0.02f);
        }
        skipButton.gameObject.SetActive(false);
    }

    public void SkipDialogue()
    {
        isDialogueSkipping = true;
        skipButton.gameObject.SetActive(false);
        StopAllCoroutines();
        if (dialogues.Count > 0)
        {
            dialogueText.text = dialogues.Dequeue();
        }
        else
        {
            dialogueText.text = "";
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        skipButton.gameObject.SetActive(false);
    }
}
