using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangerButton : MonoBehaviour
{
    public string sceneName; // the name of the scene to load

    private Button button;
    private GameManager gameManager;

    private void Start()
    {
        // get the Button component attached to this GameObject
        button = GetComponent<Button>();

        // finds the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
            return;
        }

        // adds a listener to call the LoadScene method when the button is clicked
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("Button component not found on " + gameObject.name);
        }
    }

    private void OnButtonClick()
    {
        // checks if the scene name is not empty
        if (!string.IsNullOrEmpty(sceneName))
        {
            // use the GameManager to load the specified scene
            gameManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is not specified.");
        }
    }
}

