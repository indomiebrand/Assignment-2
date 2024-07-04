using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredItemEventTrigger : MonoBehaviour
{
    public GameObject requiredItem;
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
        //    // Debug.LogError("Player not found in the scene.");
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
        if (requiredItem == null)
        {
            // Debug.LogWarning("Required item is not set in RequiredItemEventTrigger.");
            return;
        }

        if (playerInventory != null && playerInventory.HasItem(requiredItem))
        {
            triggerObject.SetActive(false);
            // Debug.Log("Player has the required item to trigger the event.");

        }
        //else
        //{
        //    // Debug.Log("Player does not have the required item to trigger the event.");
        //}
    }
}
