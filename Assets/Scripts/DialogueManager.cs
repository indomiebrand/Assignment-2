using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueBox;
    public TMP_Text dialogueText;

    private Queue<string> dialogueLines;
    private Interactable currentInteractable;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        dialogueLines = new Queue<string>();
        dialogueBox.SetActive(false); // hides the dialogue box at the start
        dialogueText.gameObject.SetActive(false); // hides the dialogue text at the start
    }

    public void StartDialogue(string[] lines, Interactable interactable)
    {
        currentInteractable = interactable;
        dialogueLines.Clear();

        foreach (string line in lines)
        {
            dialogueLines.Enqueue(line);
        }

        dialogueBox.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = dialogueLines.Dequeue();
        dialogueText.text = line;
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        currentInteractable.EndInteraction();
    }
}
