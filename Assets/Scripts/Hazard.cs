using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Transform spawnPoint; // reference to the spawn point

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player stepped onto lava.");
            RespawnPlayer(other.gameObject);
        }
    }

    private void RespawnPlayer(GameObject player)
    {
        // disables player movement
        FirstPersonController playerController = player.GetComponent<FirstPersonController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // moves the player to spawn point
        player.transform.position = spawnPoint.position;
        player.transform.rotation = Quaternion.identity; //resets rotation to 0


        // enables player movement after a short delay to avoid immediate re-triggering
        StartCoroutine(ReEnablePlayerMovement(playerController, 1.0f));
        Debug.Log("Player has respawned at the spawn point.");
    }

    private IEnumerator ReEnablePlayerMovement(FirstPersonController playerController, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
