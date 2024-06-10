using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _loopingAudioSource;
    [SerializeField] private List<SFXClip> _sfxClips;

    // Pitch variation range
    [SerializeField] private float _minPitch = 0.9f;
    [SerializeField] private float _maxPitch = 1.1f;

    private Dictionary<SFXType, List<AudioClip>> _sfxDictionary;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Death.OnPlayerDeath += StopLoopingSFX;
    }

    private void OnDisable()
    {
        Death.OnPlayerDeath -= StopLoopingSFX;
    }


    private void Init()
    {
        _sfxDictionary = new Dictionary<SFXType, List<AudioClip>>();

        foreach (var sfxClip in _sfxClips)
        {
            if (!_sfxDictionary.ContainsKey(sfxClip.Type))
            {
                _sfxDictionary[sfxClip.Type] = new List<AudioClip>();
            }

            _sfxDictionary[sfxClip.Type].Add(sfxClip.Clip);
        }
    }


    public void PlaySFX(SFXType type)
    {
        if (_sfxDictionary.ContainsKey(type))
        {
            var clips = _sfxDictionary[type];
            var clip = clips[Random.Range(0, clips.Count)];
            _sfxAudioSource.pitch = Random.Range(_minPitch, _maxPitch);
            _sfxAudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + type);
        }
    }

    public void PlayLoopingSFX(SFXType sfxType)
    {
        if (_sfxDictionary.ContainsKey(sfxType))
        {
            var clips = _sfxDictionary[sfxType];
            var clip = clips[UnityEngine.Random.Range(0, clips.Count)];
            _loopingAudioSource.clip = clip;
            _loopingAudioSource.loop = true;
            _loopingAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Looping SFX clip not found: " + sfxType);
        }
    }

    public void StopLoopingSFX()
    {
        _loopingAudioSource.Stop();
    }

    public void PlayButtonClickSound()
    {
        PlaySFX(SFXType.ButtonClick);
    }
}

[Serializable]
public class SFXClip
{
    public SFXType Type;
    public AudioClip Clip;
}

public enum SFXType
{
    PlayerAttack,
    PlayerJump,
    PlayerLand,
    PlayerHurt,
    PlayerDeath,
    PlayerRun,

    ButtonClick,
}