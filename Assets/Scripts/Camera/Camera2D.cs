using System.Collections;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Target to Follow")]
    [SerializeField] private Transform _targetTransform;
    [Header("Camera Settings")]
    [SerializeField] private Vector2 _offset = new(x: 0, y: 0); // default to center
    [SerializeField] private CameraMode _cameraMode = CameraMode.Update;    

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
}