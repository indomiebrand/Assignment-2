using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsUI;
    public GameObject creditsUI;
    public GameObject instructionsUI;
    private GameObject activeUI;

    void Start()
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

    private void SwitchUI(GameObject newUI)
    {
        if (activeUI != null)
        {
            activeUI.SetActive(false);
        }
        if (newUI != null)
        {
            newUI.SetActive(true);
        }
        activeUI = newUI;
    }
}

