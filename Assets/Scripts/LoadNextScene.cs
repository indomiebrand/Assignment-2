using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

//manages the transition to next scene based on completion of 'playabledirection' 
public class LoadNextScene : MonoBehaviour
{
    public PlayableDirector playableDirector; 
    public string nextSceneName;

    void Start() // sets up event listener if 'playableDirection' stops playing
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director) // loads next scene if 'playableDirector' stops playing
    {
        if (director == playableDirector)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void OnDestroy() // cleans up the event listener when the object is destroyed
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }
}

