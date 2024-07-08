using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake() //ensures only one instance of audiomanager exists
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // prevents the AudioSource from being destroyed when loading new scenes
        }
        else
        {
            Destroy(gameObject); // destroys any duplicate AudioManager instances
        }
    }

    //play the gameObject's audioclip at a given volume so that the audio loops continuously instead of restarting
    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        AudioSource audioSource = GetComponent<AudioSource>(); // retrieves 'audiosource' component from attached gameobject

        if (audioSource.clip != clip) //checks if there is an audio clip being played
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = true; //enables looping so that audio clip will play continuously
            audioSource.Play();
        }
    }
}

