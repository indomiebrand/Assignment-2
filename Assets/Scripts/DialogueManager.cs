using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Animator dialogueAnimator;
    public TextMeshProUGUI dialogueText;

    [HideInInspector]
    public bool isDialogueActive = false;

    private string[] dialogueLines; // an array to hold lines of dialogues
    private int currentLineIndex = 0; // used to show which line of dialogue should be shown within the array
    private KeyCode interactionKey;

    private void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    public void ShowDialogue(string[] lines, KeyCode key)
    {
        if (!isDialogueActive)
        {
            isDialogueActive = true;
            dialogueBox.SetActive(true);
            dialogueLines = lines;
            currentLineIndex = 0;
            interactionKey = key;
            StartCoroutine(PlayDialogueInAnimation());
        }
    }

    private IEnumerator PlayDialogueInAnimation()
    {
        dialogueAnimator.Play("DialogueIN");
        yield return new WaitForSeconds(dialogueAnimator.GetCurrentAnimatorStateInfo(0).length);
        dialogueText.gameObject.SetActive(true);
        DisplayLine();
    }

    private void DisplayLine()
    {
        dialogueText.text = dialogueLines[currentLineIndex];
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(interactionKey))
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            DisplayLine();
        }
        else
        {
            StartCoroutine(EndDialogue());
        }
    }

    public IEnumerator EndDialogue()
    {
        dialogueText.gameObject.SetActive(false);
        dialogueAnimator.Play("DialogueOUT");
        yield return new WaitForSeconds(dialogueAnimator.GetCurrentAnimatorStateInfo(0).length);
        dialogueBox.SetActive(false);
        isDialogueActive = false;
    }
}
