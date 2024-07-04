using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public string nextSceneName;

    void Start()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnDestroy()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}

