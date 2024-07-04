using UnityEngine;
using StarterAssets;

public class Interactable : MonoBehaviour
{
    public Canvas interactionCanvas;

    public string[] dialogueLines; // dialogue lines for interaction
    public string itemName; //name of interactable

    public bool isRequiredItem = false;
    private bool isPlayerInRange = false;
    private bool isInteracting = false;

    private FirstPersonController playerController;
    private PlayerInventory playerInventory;

    private void Start()
    {
        interactionCanvas.enabled = false; // hides the interaction canvas at start
        itemName = gameObject.name;
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
        playerController.enabled = false; // disable player movement
        interactionCanvas.enabled = true;
        DialogueManager.Instance.StartDialogue(dialogueLines, this);
    }

    private void ContinueDialogue()
    {
        DialogueManager.Instance.DisplayNextLine(); // Display next line of dialogue
    }

    public void EndInteraction()
    {
        if (isInteracting)
        {
            isInteracting = false;
            interactionCanvas.enabled = false;
            playerController.enabled = true; // enable player movement again

            if (isRequiredItem)
            {
                GameObject itemObject = GameObject.Find(itemName);
                if (itemObject != null)
                {
                    playerInventory.AddItem(itemObject);
                    itemObject.SetActive(false); // Deactivate the GameObject instead of destroying it
                }
                else
                {
                    Debug.LogWarning("Item GameObject not found with name: " + itemName);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerController = other.GetComponent<FirstPersonController>();
            playerInventory = other.GetComponent<PlayerInventory>();
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
