using UnityEngine;
using StarterAssets;

public class Interactable : MonoBehaviour
{
    public Canvas interactionCanvas;

    public string[] dialogueLines; // dialogue lines for interaction
    public string itemName; //name of interactable

    public bool isRequiredItem = false;
    public bool LoadNextScene = false;
    private bool isPlayerInRange = false;
    private bool isInteracting = false;

    private FirstPersonController playerController;
    private PlayerInventory playerInventory;
    public GameManager gameManager;

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

        if (LoadNextScene)
        {
            gameManager.LoadNextScene();
        }
        else
        {
            DialogueManager.Instance.StartDialogue(dialogueLines, this);
        }
    }

    private void ContinueDialogue()
    {
        DialogueManager.Instance.DisplayNextLine(); // displays next line of dialogue
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
                    itemObject.SetActive(false); // deactivates the gameobject
                }
                //else
                //{
                //    Debug.LogWarning("Item GameObject not found with name: " + itemName);
                //}
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
            interactionCanvas.enabled = true; // shows interaction canvas
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionCanvas.enabled = false; // hides interaction canvas
            EndInteraction();
        }
    }
}
