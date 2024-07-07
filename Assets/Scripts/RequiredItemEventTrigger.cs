using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredItemEventTrigger : MonoBehaviour
{
    public GameObject[] requiredItems; // array of required items that the player must have
    public GameObject eventObject; // the object to trigger when all required items are present

    private Interactable interactable;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        if (interactable == null)
        {
            Debug.LogError("Interactable component not found on " + gameObject.name);
        }
    }

    public void CheckRequiredItems(PlayerInventory playerInventory)
    {
        bool allItemsPresent = true;

        // check if all required items are in the player's inventory
        foreach (GameObject requiredItem in requiredItems)
        {
            if (!playerInventory.HasItem(requiredItem))
            {
                allItemsPresent = false;
                break;
            }
        }

        if (allItemsPresent)
        {
            eventObject.SetActive(false);
            Debug.Log("Player has all required items. Triggering event.");

            // complete interaction if interactable is set
            if (interactable != null)
            {
                interactable.interactionButton.gameObject.SetActive(false);
                interactable.EndInteraction(); // end any interaction if its ongoing
            }
        }
        else
        {
            // player does not have all required items
            Debug.Log("Player does not have all required items.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                CheckRequiredItems(playerInventory);
            }
            else
            {
                Debug.LogWarning("PlayerInventory component not found on player.");
            }
        }
    }
}