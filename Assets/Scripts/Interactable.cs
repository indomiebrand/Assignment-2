using UnityEngine;
using StarterAssets;

public class Interactable : MonoBehaviour
{
    public Canvas interactionCanvas;
    public string[] dialogueLines; // Dialogue lines for interaction
    public bool isRequiredItem = false;
    private bool isPlayerInRange = false;
    private bool isInteracting = false;
    private FirstPersonController playerController;

    private void Start()
    {
        interactionCanvas.enabled = false; // Hide the interaction canvas at start
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isInteracting)
            {
                StartInteraction();
            }
            else
            {
                ContinueDialogue();
            }
        }
    }

    private void StartInteraction()
    {
        isInteracting = true;
        playerController.enabled = false; // Disable player movement
        interactionCanvas.enabled = true;
        DialogueManager.Instance.StartDialogue(dialogueLines, this);
    }

    private void ContinueDialogue()
    {
        DialogueManager.Instance.DisplayNextLine(); // Display next line of dialogue
    }

    public void EndInteraction()
    {
        isInteracting = false;
        interactionCanvas.enabled = false;
        playerController.enabled = true; // Enable player movement again

        if (isRequiredItem)
        {
            Destroy(gameObject); // Destroy the game object if it is a required item
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerController = other.GetComponent<FirstPersonController>();
            interactionCanvas.enabled = true; // Show interaction canvas
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionCanvas.enabled = false; // Hide interaction canvas
            EndInteraction();
        }
    }
}
