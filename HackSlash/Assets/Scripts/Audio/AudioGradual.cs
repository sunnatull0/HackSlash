using UnityEngine;

public class AudioGradual : MonoBehaviour
{
    [SerializeField] private SFXType _type;
    [SerializeField] private float _minInterval = 3.0f;
    [SerializeField] private float _maxInterval = 5.0f;
    private float _nextPlayTime;

    private void Start()
    {
        ScheduleNextPlay();
    }

    private void Update()
    {
        if (Time.time >= _nextPlayTime)
        {
            SFXManager.Instance.PlaySFX(_type);
            ScheduleNextPlay();
        }
    }

    private void ScheduleNextPlay()
    {
        float interval = Random.Range(_minInterval, _maxInterval);
        _nextPlayTime = Time.time + interval;
    }
}