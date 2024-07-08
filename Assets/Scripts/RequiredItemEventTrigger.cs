using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredItemEventTrigger : MonoBehaviour
{
    public GameObject[] requiredItems; // array of required items that the player must have
    public GameObject eventObject; //  object to trigger when all required items are present
    private PlayerInventory playerInventory; // ref to the player's inventory

    private void Start()
    {
        if (eventObject != null)
        {
            eventObject.SetActive(false); // makes sure the eventObject is inactive at the start
        }

        // finds the player inventory in the scene
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory not found in the scene.");
        }
    }

    private void Update()
    {
        // checks if the player has all required items periodically
        if (playerInventory != null)
        {
            CheckRequiredItems(playerInventory);
        }
    }

    public void CheckRequiredItems(PlayerInventory playerInventory)
    {
        bool allItemsPresent = true;

        // checks if all required items are in the player's inventory
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

            if (eventObject != null)
            {
                Debug.Log("Player has all required items. Triggering event.");
                eventObject.SetActive(true); // makes the eventObject appear
            }
        }
        else
        {
            // if player does not have all required items
            Debug.Log("Player does not have all required items.");
        }
    }
}
