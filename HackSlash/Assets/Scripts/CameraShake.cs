using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private float _timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Shake(float amplitude, float time)
    {
        var basicChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        basicChannelPerlin.m_AmplitudeGain = amplitude;

        _timer = time;
    }

    private void Update()
    {
        if (_timer < 0f)
            return;

        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            var basicChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            basicChannelPerlin.m_AmplitudeGain = 0f;
        }
    }
}