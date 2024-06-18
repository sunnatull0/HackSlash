using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public static VolumeControl Instance;

    [SerializeField] private Slider backgroundMusicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField]
    private List<AudioSource> backgroundMusicSources; // List to hold multiple background music audio sources

    [SerializeField] private List<AudioSource> sfxSources; // List to hold multiple SFX audio sources

    private float previousBackgroundVolume;
    private float previousSFXVolume;

    private readonly string _backgroundVolumeString = "BackgroundVolume";
    private readonly string _sfxVolumeString = "sfxVolume";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadVolumeSettings();
    }

    private void Start()
    {
        if (backgroundMusicSources.Count > 0)
        {
            backgroundMusicSlider.value = backgroundMusicSources[0].volume;
            previousBackgroundVolume = backgroundMusicSources[0].volume;
        }

        if (sfxSources.Count > 0)
        {
            sfxSlider.value = sfxSources[0].volume;
            previousSFXVolume = sfxSources[0].volume;
        }

        backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBackgroundMusicVolume(float value)
    {
        foreach (var backgroundMusicSource in backgroundMusicSources)
        {
            backgroundMusicSource.volume = value;
        }

        if (Mathf.Abs(value - previousBackgroundVolume) >= 0.1f)
        {
            previousBackgroundVolume = value;
        }

        if (BackgroundAudio.Instance != null)
        {
            if (BackgroundAudio.Instance._currentAmbienceCoroutine != null)
            {
                BackgroundAudio.Instance.StopCoroutine(BackgroundAudio.Instance._currentAmbienceCoroutine);
            }

            if (BackgroundAudio.Instance._currentRegularCoroutine != null)
            {
                BackgroundAudio.Instance.StopCoroutine(BackgroundAudio.Instance._currentRegularCoroutine);
            }

            if (BackgroundAudio.Instance._currentBossCoroutine != null)
            {
                BackgroundAudio.Instance.StopCoroutine(BackgroundAudio.Instance._currentBossCoroutine);
            }
        }
    }

    private void SetSFXVolume(float value)
    {
        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = value;
        }

        if (Mathf.Abs(value - previousSFXVolume) >= 0.1f)
        {
            previousSFXVolume = value;
        }
    }


    public void SaveVolumeSettings()
    {
        if (backgroundMusicSources.Count > 0)
        {
            PlayerPrefs.SetFloat(_backgroundVolumeString, backgroundMusicSlider.value);
        }

        if (sfxSources.Count > 0)
        {
            PlayerPrefs.SetFloat(_sfxVolumeString, sfxSources[0].volume);
        }

        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        var backgroundVolume = PlayerPrefs.GetFloat(_backgroundVolumeString, 0.8f);
        var sfxVolume = PlayerPrefs.GetFloat(_sfxVolumeString, 0.8f);

        Debug.Log(backgroundVolume);
        foreach (var backgroundMusicSource in backgroundMusicSources)
        {
            backgroundMusicSource.volume = backgroundVolume;
        }

        foreach (var sfxSource in sfxSources)
        {
            sfxSource.volume = sfxVolume;
        }
        
        backgroundMusicSlider.value = backgroundVolume;
        sfxSlider.value = sfxVolume;
    }
}