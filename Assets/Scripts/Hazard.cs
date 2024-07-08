using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using StarterAssets;

public class Hazard : MonoBehaviour
{
    public Transform spawnPoint; // reference to the spawn point
    public GameObject deathUI; // reference to the Death UI canvas
    public Button restartButton; // reference to the Restart button
    public Button mainMenuButton; // reference to the Main Menu button

    private void Start()
    {
        // hides the death UI at the start
        if (deathUI != null)
        {
            deathUI.SetActive(false);
        }

        // adds listeners to the buttons
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(Restart);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(MainMenu);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player stepped onto lava.");
            ShowDeathUI(other.gameObject);
        }
    }

    private void ShowDeathUI(GameObject player)
    {
        // disables player movement
        FirstPersonController playerController = player.GetComponent<FirstPersonController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // shows the death UI
        if (deathUI != null)
        {
            deathUI.SetActive(true);
        }

        // unlocks the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        // finds the player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // moves the player to spawn point
            player.transform.position = spawnPoint.position;
            player.transform.rotation = Quaternion.identity; // resets rotation to 0

            // enables player movement after a short delay to avoid immediate re-triggering
            FirstPersonController playerController = player.GetComponent<FirstPersonController>();
            StartCoroutine(ReEnablePlayerMovement(playerController, 1.0f));
            Debug.Log("Player has respawned at the spawn point.");
        }

        // hides the death UI
        if (deathUI != null)
        {
            deathUI.SetActive(false);
        }

        // locks the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        // locks the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // loads the main menu scene
        SceneManager.LoadScene("StartMenu");
    }

    //used to add a delay before re-enabling player's movement after player respawns
    private IEnumerator ReEnablePlayerMovement(FirstPersonController playerController, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
