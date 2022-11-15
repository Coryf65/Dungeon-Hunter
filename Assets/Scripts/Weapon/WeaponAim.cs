using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private GameObject _reticlePrefab;

    private Camera _mainCamera;
    private GameObject _reticle;
    private Weapon _weapon;
    private Vector3 _direction;
    private Vector3 _mousePosition;
    private Vector3 _reticlePosition;
    private Vector3 _currentAim = Vector3.zero;
    private Vector3 _currentAimAbsolute = Vector3.zero;
    private Quaternion _initialRotation;
    private Quaternion _lookRotation;

    public float CurrentAimAngleAbsolute { get; set; }
    public float CurrentAimAngle { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        _weapon = GetComponent<Weapon>();
        _initialRotation = transform.rotation;
        _mainCamera = Camera.main;
        _reticle = Instantiate(_reticlePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        MoveReticle();
        RotateWeapon();
    }

    private void GetMousePosition()
    {
        _mousePosition = Input.mousePosition;
        _mousePosition.z = 5f; // clamping the z value to 5

        _direction = _mainCamera.ScreenToWorldPoint(_mousePosition);
        _direction.z = transform.position.z;

        _reticlePosition = _direction;

        _currentAimAbsolute = _direction - transform.position;

        if (_weapon.WeaponUser.GetComponent<SpriteFlip>().FacingRight)
        {
            _currentAim = _direction - transform.position;
        }
        else
        {
            _currentAim = transform.position - _direction;
        }
    }

    /// <summary>
    /// Moves our reticle towards our mouse position
    /// </summary>
    private void MoveReticle()
    {
        _reticle.transform.rotation = Quaternion.identity;
        _reticle.transform.position = _reticlePosition;
    }

    private void RotateWeapon()
    {
        if (_currentAim != Vector3.zero && _direction != Vector3.zero)
        {
            // store the angles
            CurrentAimAngle = Mathf.Atan2(y: _currentAim.y, x: _currentAim.x) * Mathf.Rad2Deg;
            CurrentAimAngleAbsolute = Mathf.Atan2(y: _currentAimAbsolute.y, x: _currentAimAbsolute.x) * Mathf.Rad2Deg;

            // Clamping our angle
            if (_weapon.WeaponUser.GetComponent<SpriteFlip>().FacingRight)
            {
                CurrentAimAngle = Mathf.Clamp(value: CurrentAimAngle, min: -180, max: 180);
            }
            else
            {
                CurrentAimAngle = Mathf.Clamp(value: CurrentAimAngle, min: -180, max: 180);
            }

            // applying angle
            _lookRotation = Quaternion.Euler(CurrentAimAngle * Vector3.forward);
            transform.rotation = _lookRotation;
        }
        else
        {
            // not moving
            CurrentAimAngle = 0;
            transform.rotation = _initialRotation;
        }
    }
}
