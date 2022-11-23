using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DShaker : MonoBehaviour
{
    [Header("Turn on/off the Camera Shaker")]
    [SerializeField] private bool _UseCameraShake = true;
    [Header("Camera2D Shaker Settings")]
    [SerializeField] private float _shakeVibrato = 0.1f;
    [SerializeField] private float _shakeRandomness = 0.01f;
    [SerializeField] private float _shakeDuration = 0.01f;

    public void ShakeCamera()
    {
        StartCoroutine(IShaker());
    }

    /// <summary>
    /// Coroutine to shake the camera
    /// </summary>
    /// <returns></returns>
    private IEnumerator IShaker()
    {
        Vector3 currentPosition = transform.position;

        for (int i = 0; i < _shakeVibrato; i++)
        {
            Vector3 shakePosition = currentPosition + Random.onUnitSphere * _shakeRandomness;
            yield return new WaitForSeconds(_shakeDuration);
            transform.position = shakePosition;
        }
        yield return null;
    }

    private void OnShooting()
    {
        ShakeCamera();
    }

    private void OnEnable()
    {
        if (_UseCameraShake)
        {
            CharacterWeapon.OnStartShooting += OnShooting;
        }
    }

    private void OnDisable()
    {
        if (_UseCameraShake)
        {
            CharacterWeapon.OnStartShooting -= OnShooting;
        }
    }
}