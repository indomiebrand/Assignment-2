using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Button interactionButton; // button for interaction
    public string[] dialogueLines; // dialogue lines for interaction
    public bool loadNextScene = false; // flag to load the next scene

    private bool isPlayerInRange = false;
    private bool isInteracting = false;

    private FirstPersonController playerController;
    private PlayerInventory playerInventory;
    private GameManager gameManager;
    private RequiredItem requiredItem;

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

        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        requiredItem = GetComponent<RequiredItem>();
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
        if (!isInteracting)
        {
            isInteracting = true;
            playerController.enabled = false; // disable player movement

            if (interactionButton != null)
            {
                interactionButton.gameObject.SetActive(false); // hides the interaction button
            }
            else
            {
                Debug.LogWarning("Interaction button is missing.");
            }

            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(dialogueLines, this); // start dialogue
            }
            else
            {
                Debug.LogWarning("DialogueManager not found.");
            }

            Debug.Log("Interaction started, player movement disabled.");
        }
        else
        {
            ContinueDialogue();
        }

        // Check if the scene should be loaded
        if (loadNextScene)
        {
            gameManager.LoadNextScene();
        }
    }

    public void ContinueDialogue()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.DisplayNextLine(); // display next line of dialogue
            Debug.Log("Continuing dialogue.");
        }
        else
        {
            Debug.LogWarning("DialogueManager not found.");
        }
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

            playerController.enabled = true; // enable player movement
            Debug.Log("Player movement enabled.");

            if (requiredItem != null)
            {
                requiredItem.HandleInteraction(playerInventory); // handle required item interaction
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
            EndInteraction();
        }
    }
}
