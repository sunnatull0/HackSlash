using System.Collections;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public static BackgroundAudio Instance;

    [SerializeField] private AudioSource _ambienceSource;
    [SerializeField] private AudioSource _regularSource;
    [SerializeField] private AudioSource _bossSource;

    public Coroutine _currentAmbienceCoroutine;
    public Coroutine _currentRegularCoroutine;
    public Coroutine _currentBossCoroutine;

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
    }

    private void Start()
    {
        PlayAmbienceMusic();
    }

    public void PlayAmbienceMusic()
    {
        if (_currentAmbienceCoroutine != null)
        {
            StopCoroutine(_currentAmbienceCoroutine);
        }
        _currentAmbienceCoroutine = StartCoroutine(FadeIn(_ambienceSource, 1.0f));
    }

    public void StopAmbienceMusic()
    {
        if (_currentAmbienceCoroutine != null)
        {
            StopCoroutine(_currentAmbienceCoroutine);
        }
        _currentAmbienceCoroutine = StartCoroutine(FadeOut(_ambienceSource, 1.0f));
    }

    public void PlayRegularMusic()
    {
        if (_currentRegularCoroutine != null)
        {
            StopCoroutine(_currentRegularCoroutine);
        }
        _currentRegularCoroutine = StartCoroutine(FadeIn(_regularSource, 1.0f));
    }

    public void StopRegularMusic()
    {
        if (_currentRegularCoroutine != null)
        {
            StopCoroutine(_currentRegularCoroutine);
        }
        _currentRegularCoroutine = StartCoroutine(FadeOut(_regularSource, 1.0f));
    }

    public void PlayBossMusic()
    {
        if (_currentBossCoroutine != null)
        {
            StopCoroutine(_currentBossCoroutine);
        }
        _currentBossCoroutine = StartCoroutine(FadeIn(_bossSource, 1.0f));
    }

    public void StopBossMusic()
    {
        if (_currentBossCoroutine != null)
        {
            StopCoroutine(_currentBossCoroutine);
        }
        _currentBossCoroutine = StartCoroutine(FadeOut(_bossSource, 1.0f));
    }

    private IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        if (audioSource != null)
        {
            var previousVolume = audioSource.volume;
            audioSource.volume = 0f;
            audioSource.Play();
            while (audioSource.volume < previousVolume)
            {
                audioSource.volume += Time.deltaTime / fadeTime;
                yield return null;
            }

            audioSource.volume = previousVolume;
        }
    }

    private IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        if (audioSource != null)
        {
            float startVolume = audioSource.volume;
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume; // Reset volume for future use
        }
    }
}
