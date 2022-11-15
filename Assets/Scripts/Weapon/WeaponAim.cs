using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private GameObject _reticlePrefab;

    private Camera _mainCamera;
    private GameObject _reticle;
    private Vector3 _direciton;
    private Vector3 _mousePosition;
    private Vector3 _reticlePosition;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        _mainCamera = Camera.main;
        _reticle = Instantiate(_reticlePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        MoveReticle();
    }

    private void GetMousePosition()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition.z = 5f; // clamping the z value to 5

        _direciton = _mainCamera.ScreenToWorldPoint(_mousePosition);
        _direciton.z = transform.position.z;

        _reticlePosition = _direciton;
    }

    private void MoveReticle()
    {
        _reticle.transform.rotation = Quaternion.identity;
        _reticle.transform.position = _reticlePosition;
    }
}
