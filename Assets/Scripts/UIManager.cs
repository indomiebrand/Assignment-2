using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles the toggling of the UI in the start menu
public class MenuManager : MonoBehaviour
{
    public GameObject settingsUI;
    public GameObject creditsUI;
    public GameObject instructionsUI;
    private GameObject activeUI;

    void Start() //initializes all UI panels to be inactive
    {
        settingsUI.SetActive(false);
        creditsUI.SetActive(false);
        instructionsUI.SetActive(false);
    }

    public void ShowSettings()
    {
        SwitchUI(settingsUI);
    }

    public void ShowCredits()
    {
        SwitchUI(creditsUI);
    }

    public void ShowInstructions()
    {
        SwitchUI(instructionsUI);
    }

    private void SwitchUI(GameObject newUI) //responsible for toggling active UI panel
    {
        if (activeUI != null) //if there is an 'activeUI' panel, it is set to inactive
        {
            activeUI.SetActive(false);
        }
        if (newUI != null) // the new UIpanel is set to active
        {
            newUI.SetActive(true);
        }
        activeUI = newUI; // 'activeUI' is updated to new UI panel
    }
}

