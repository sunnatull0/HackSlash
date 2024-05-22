using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private float _zoomOutSize = 32f;
    [SerializeField] private float _zoomInSize = 29f;
    [SerializeField] private float _duration = 1f;

    private void Start()
    {
        Death.OnPlayerDeath += ZoomIn;
        _camera.m_Lens.OrthographicSize = _zoomInSize;
    }

    private void OnDisable()
    {
        Death.OnPlayerDeath -= ZoomIn;
    }

    private void ZoomIn()
    {
        StartCoroutine(Zoom(_zoomInSize));
    }

    public void ZoomOut()
    {
        StartCoroutine(Zoom(_zoomOutSize));
    }

    private IEnumerator Zoom(float targetSize)
    {
        float startSize = _camera.m_Lens.OrthographicSize;
        float elapsedTime = 0f;

        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            _camera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsedTime / _duration);
            yield return null;
        }

        _camera.m_Lens.OrthographicSize = targetSize;
    }
}
