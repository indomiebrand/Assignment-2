using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public string volumePrefKey = "volume";

    void Start()
    {
        if (PlayerPrefs.HasKey(volumePrefKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(volumePrefKey);
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume);
        }
        else
        {
            volumeSlider.value = 0.5f;
            SetVolume(0.5f);
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(volumePrefKey, volume);
    }
}

