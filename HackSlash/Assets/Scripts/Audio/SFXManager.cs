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

    private Dictionary<SFXType, List<SFXClip>> _sfxDictionary;


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
        _sfxDictionary = new Dictionary<SFXType, List<SFXClip>>();

        foreach (var sfxClip in _sfxClips)
        {
            if (!_sfxDictionary.ContainsKey(sfxClip.Type))
            {
                _sfxDictionary[sfxClip.Type] = new List<SFXClip>();
            }

            _sfxDictionary[sfxClip.Type].Add(sfxClip);
        }
    }


    public void PlaySFX(SFXType type)
    {
        if (_sfxDictionary.ContainsKey(type))
        {
            var clips = _sfxDictionary[type];
            var sfxClip = clips[Random.Range(0, clips.Count)];

            if (!_sfxAudioSource.isPlaying)
            {
                _sfxAudioSource.pitch = Random.Range(_minPitch, _maxPitch);
            }

            _sfxAudioSource.PlayOneShot(sfxClip.Clip, sfxClip.Volume);
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
            var sfxClip = clips[UnityEngine.Random.Range(0, clips.Count)];
            _loopingAudioSource.clip = sfxClip.Clip;
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

    public void PlayPopUpSound()
    {
        PlaySFX(SFXType.PopUp);
    }

    public void PlayWhooshSound()
    {
        PlaySFX(SFXType.Whoosh);
    }
}

[Serializable]
public class SFXClip
{
    public SFXType Type;
    public AudioClip Clip;
    public float Volume = 1.0f;
}

public enum SFXType
{
    // PlayerFolder
    PlayerAttack,
    PlayerJump,
    PlayerLand,
    PlayerHurt,
    PlayerDeath,
    PlayerRun,

    // UI and WORLD
    ButtonClick,
    PopUp,
    Whoosh,
    SliderChange,
    BossFinished,

    // Bat
    BatDeath,
    BatHurt,
    BatScreech,

    // Bear
    BearDeath,
    BearHurt,
    BearAttack,
    BearGrunt,

    // Boar
    BoarDeath,
    BoarHurt,
    BoarAttack,
    BoarHitPlayer,

    // Orc
    OrcDeath,
    OrcHurt,
    OrcAttack,
    OrcJump,
    OrcLand,
    OrcSpawn,
    OrcGrunt,

    // Troll
    TrollDeath,
    TrollWalk,
    TrollHurt,
    TrollHurtNot,
    TrollAttack,
    TrollAttackBoss,
    TrollAttackClub,
    TrollSpawn,
    TrollSpawnBoss,
    TrollGrunt,
    
    WaveSound
}