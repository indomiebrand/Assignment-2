using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // load saved volume from GameManager
        float savedVolume = GameManager.Instance.GetSavedVolume();
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        GameManager.Instance.SaveVolume(volume); // saves volume to GameManager
    }
}

