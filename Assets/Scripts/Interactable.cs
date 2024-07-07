using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class Interactable : MonoBehaviour
{
    public Button interactionButton; // button for interaction
    public string[] dialogueLines; // dialogue lines for interaction
    private string itemName; // name of interactable

    public bool isRequiredItem = false;
    public bool LoadNextScene = false;
    private bool isPlayerInRange = false;
    private bool isInteracting = false;

    private FirstPersonController playerController;
    private PlayerInventory playerInventory;
    public GameManager gameManager;

    private void Start()
    {
        if (interactionButton != null)
        {
            interactionButton.gameObject.SetActive(false); // hide the interaction button at start
        }
        else
        {
            Debug.LogWarning("Interaction button is missing.");
        }

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

    public void StartInteraction() 
    {
        isInteracting = true;
        playerController.enabled = false; // disable player movement
        interactionButton.gameObject.SetActive(false); // hides the interaction button
        Debug.Log("Interaction started, player movement disabled.");


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
        DialogueManager.Instance.DisplayNextLine(); // display next line of dialogue
        Debug.Log("Continuing dialogue.");
    }

    public void EndInteraction()
    {
        if (isInteracting)
        {
            isInteracting = false;

            if (interactionButton != null)
            {
                interactionButton.gameObject.SetActive(false); // hide the interaction button
            }
            else
            {
                Debug.LogWarning("Interaction button is missing.");
            }

            playerController.enabled = true; // enable player movement again
            Debug.Log("Player movement enabled.");

            if (isRequiredItem)
            {
                GameObject itemObject = GameObject.Find(itemName);
                if (itemObject != null)
                {
                    playerInventory.AddItem(itemObject);
                    itemObject.SetActive(false); // deactivate the gameobject
                    Debug.Log("Added to inventory.");
                }
                else
                {
                    Debug.LogWarning(itemName + " not found.");
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
            Debug.Log("Player entered trigger.");

            if (interactionButton != null)
            {
                interactionButton.gameObject.SetActive(true); // show interaction button
                //Debug.Log("Interaction button shown on player enter.");
            }
            else
            {
                Debug.LogWarning("Interaction button is missing.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //Debug.Log("Player exited trigger.");

            if (interactionButton != null)
            {
                interactionButton.gameObject.SetActive(false); // hide interaction button
                //Debug.Log("Interaction button hidden on player exit.");
            }
            else
            {
                Debug.LogWarning("Interaction button is missing.");
            }

            EndInteraction();
        }
    }
}
