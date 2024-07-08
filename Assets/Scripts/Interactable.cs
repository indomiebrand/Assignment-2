using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

//mainly handles the interaction throughout the game
public class Interactable : MonoBehaviour
{
    public Button interactionButton; // button for interaction
    public string[] dialogueLines; // dialogue lines for interaction
    public bool loadNextScene = false; // flag to load the next scene

    private bool isPlayerInRange = false; // flag to indicate if player is within interaction range
    private bool isInteracting = false; // flag to indicate if an interaction is taking place

    //references other variables from other scripts
    private FirstPersonController playerController;
    private PlayerInventory playerInventory;
    private GameManager gameManager;
    private RequiredItem requiredItem;
    public AudioSource interactionSound;

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

        // finds the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        requiredItem = GetComponent<RequiredItem>();

        // finds the AudioSource named "Interaction SFX" in the hierarchy
        GameObject interactionSFXObject = GameObject.Find("Interaction SFX");
        if (interactionSFXObject != null)
        {
            interactionSound = interactionSFXObject.GetComponent<AudioSource>();
            if (interactionSound == null)
            {
                Debug.LogWarning("No AudioSource found on the 'Interaction SFX' GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("No GameObject named 'Interaction SFX' found in the hierarchy.");
        }
    }

    private void Update() // listens for player inputs and starts/continues interaction
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

            // plays interaction sound
            if (interactionSound != null)
            {
                interactionSound.Play();
            }
            else
            {
                Debug.LogWarning("Interaction sound is missing.");
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
                interactionButton.gameObject.SetActive(false); // hides the interaction button upon interaction
            }
            else
            {
                Debug.LogWarning("Interaction button is missing.");
            }

            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.StartDialogue(dialogueLines, this); // start dialogue and runs through the lines
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

        // loads next scene if loadNextScene returns true
        if (loadNextScene)
        {
            gameManager.LoadNextScene();
        }
    }

    public void ContinueDialogue() //displays next line of dialogue
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

    public void EndInteraction() //ends interaction and re-enables player movement
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
            if (interactionButton != null)
            {
                interactionButton.gameObject.SetActive(false); // hide the interaction button
            }
            else
            {
                Debug.LogWarning("Interaction button is missing.");
            }

            isPlayerInRange = false;
            EndInteraction();
        }
    }
}
