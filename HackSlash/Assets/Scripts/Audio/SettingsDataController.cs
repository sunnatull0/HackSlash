using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class SettingsDataController : MonoBehaviour
    {
        [SerializeField] private Slider backgroundMusicSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider hudControl;

        [SerializeField]
        private List<AudioSource> backgroundMusicSources; // List to hold multiple background music audio sources

        [SerializeField] private List<AudioSource> sfxSources; // List to hold multiple SFX audio sources

        private float _previousBackgroundVolume;
        private float _previousSfxVolume;

        private readonly string _backgroundVolumeString = "BackgroundVolume";
        private readonly string _sfxVolumeString = "sfxVolume";
        private readonly string _hudValueString = "hudValue";

        private void Awake()
        {
            LoadSettings();
        }

        private void Start()
        {
            // Background Music Value settings.
            if (backgroundMusicSources.Count > 0)
            {
                backgroundMusicSlider.value = backgroundMusicSources[0].volume;
                _previousBackgroundVolume = backgroundMusicSources[0].volume;
            }

            // SFX Value settings.
            if (sfxSources.Count > 0)
            {
                sfxSlider.value = sfxSources[0].volume;
                _previousSfxVolume = sfxSources[0].volume;
            }

            backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void SetBackgroundMusicVolume(float value)
        {
            foreach (var backgroundMusicSource in backgroundMusicSources)
            {
                backgroundMusicSource.volume = value;
            }

            if (Mathf.Abs(value - _previousBackgroundVolume) >= 0.1f)
            {
                _previousBackgroundVolume = value;
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

        private void SetSfxVolume(float value)
        {
            foreach (var sfxSource in sfxSources)
            {
                sfxSource.volume = value;
            }

            if (Mathf.Abs(value - _previousSfxVolume) >= 0.1f)
            {
                _previousSfxVolume = value;
            }
        }


        private void SaveSettings()
        {
            // Background Music.
            if (backgroundMusicSources.Count > 0)
            {
                PlayerPrefs.SetFloat(_backgroundVolumeString, backgroundMusicSlider.value);
            }

            // SFX.
            if (sfxSources.Count > 0)
            {
                PlayerPrefs.SetFloat(_sfxVolumeString, sfxSources[0].volume);
            }
        
            // HUD controller.
            PlayerPrefs.SetFloat(_hudValueString, hudControl.value);
        
            PlayerPrefs.Save();
        }

        private void LoadSettings()
        {
            var backgroundVolume = PlayerPrefs.GetFloat(_backgroundVolumeString, 0.8f);
            var sfxVolume = PlayerPrefs.GetFloat(_sfxVolumeString, 0.8f);

            foreach (var backgroundMusicSource in backgroundMusicSources)
            {
                backgroundMusicSource.volume = backgroundVolume;
            }

            foreach (var sfxSource in sfxSources)
            {
                sfxSource.volume = sfxVolume;
            }
        
            hudControl.value = PlayerPrefs.GetFloat(_hudValueString);
            backgroundMusicSlider.value = backgroundVolume;
            sfxSlider.value = sfxVolume;
        }

        private void OnDestroy()
        {
            SaveSettings();
        }
    }
}