using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
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

    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}

