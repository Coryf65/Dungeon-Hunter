using System.Collections;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] private Transform _targetTransform;
    [Header("Camera Settings")]
    [SerializeField] private Vector2 _offset = new(x: 0, y: 0); // default to center
    [SerializeField] private CameraMode _cameraMode = CameraMode.Update;
    [SerializeField] private bool _UseCameraShake = false;
    [SerializeField] private float _shakeVibrato = 10f;
    [SerializeField] private float _shakeRandomness = 0.1f;
    [SerializeField] private float _shakeDuration = 0.01f;

    private enum CameraMode
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    private void Update()
    {
        if (_cameraMode == CameraMode.Update)
        {
            FollowTarget();
        }
    }

    private void FixedUpdate()
    {
        if (_cameraMode == CameraMode.FixedUpdate)
        {
            FollowTarget();
        }
    }

    private void LateUpdate()
    {
        if (_cameraMode == CameraMode.LateUpdate)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        Vector3 desiredPosition = new(
            x: _targetTransform.position.x + _offset.x,
            y: _targetTransform.position.y + _offset.y,
            z: transform.position.z
        );

        transform.position = desiredPosition;
    }

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