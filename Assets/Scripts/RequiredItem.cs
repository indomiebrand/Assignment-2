using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequiredItem : MonoBehaviour
{
    public bool isRequiredItem = false; // flag to determine if this is a required item

    private void Start()
    {
        if (isRequiredItem)
        {
            gameObject.SetActive(true); // ensure the gameobject is active if it's a required item
        }
    }

    public void HandleInteraction(PlayerInventory playerInventory)
    {
        if (isRequiredItem)
        {
            playerInventory.AddItem(gameObject); // add this object to the player's inventory
            gameObject.SetActive(false); // deactivate the gameobject after adding it to inventory
        }
    }
}
