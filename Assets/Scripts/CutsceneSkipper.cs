using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSkipper : MonoBehaviour
{
    private KeyCode skipKey = KeyCode.E; // key to skip the cutscene

    private void Update()
    {
        if (Input.GetKeyDown(skipKey))
        {
            SkipCutscene();
        }
    }

    private void SkipCutscene()
    {
        GameManager.Instance.LoadNextScene();
    }
}

