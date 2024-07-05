using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredItemEventTrigger : MonoBehaviour
{
    public GameObject[] requiredItems; // array to hold multiple required items
    public GameObject triggerObject;

    private Interactable interactableScript;
    private PlayerInventory playerInventory;

    private void Start()
    {
        interactableScript = GetComponent<Interactable>();
        if (interactableScript == null)
        {
            // Debug.LogError("Interactable script not found on the same GameObject as RequiredItemEventTrigger.");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInventory = player.GetComponent<PlayerInventory>();
        }
        //else
        //{
        //    // Debug.LogError("Player GameObject not found in the scene.");
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckTriggerEvent();
        }
    }

    private void CheckTriggerEvent()
    {
        if (requiredItems == null || requiredItems.Length == 0)
        {
            // Debug.LogWarning("Required items are not set in RequiredItemEventTrigger.");
            return;
        }

        bool hasAllItems = true;
        foreach (GameObject item in requiredItems)
        {
            if (!playerInventory.HasItem(item))
            {
                hasAllItems = false;
                break;
            }
        }

        if (hasAllItems)
        {
            triggerObject.SetActive(false);
            // Debug.Log("Player has all the required items to trigger the event.");
        }
        //else
        //{
        //    // Debug.Log("Player does not have all the required items to trigger the event.");
        //}
    }
}
