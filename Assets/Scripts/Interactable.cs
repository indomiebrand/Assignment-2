using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Canvas interactCanvas;
    public string[] dialogueLines; // an array to hold dialogue lines for the specific gameobject(s)
    public KeyCode interactionKey = KeyCode.E;
    public DialogueManager dialogueManager; // references the DialogueManager script

    private bool isPlayerInRange = false; // to track if the player is in range

    void Start()
    {
        if (interactCanvas != null)
        {
            interactCanvas.gameObject.SetActive(false); // hides the canvas at the start if it's not hidden
        }

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // sets player in range flag to true
            Debug.Log("OnTriggerEnter called with: " + other.name);
            Debug.Log("Activating Canvas");
            interactCanvas.gameObject.SetActive(true); // shows the canvas when the player enters the trigger
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(interactionKey))
        {
            if (dialogueManager != null && !dialogueManager.isDialogueActive)
            {
                dialogueManager.ShowDialogue(dialogueLines, interactionKey);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // sets player in range flag to false
            Debug.Log("OnTriggerExit called with: " + other.name);
            Debug.Log("Deactivating Canvas");
            interactCanvas.gameObject.SetActive(false); // hides the canvas when the player exits the trigger
            dialogueManager.EndDialogue(); // ends dialogue using DialogueManager script
        }
    }
}
