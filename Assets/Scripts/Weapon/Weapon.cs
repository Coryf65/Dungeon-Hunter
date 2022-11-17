using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("settings")]
    [Tooltip("The Time between shots. How long between each shot will take.")]
    [Range(0f, 10f)]
    [SerializeField] private float _fireRate = 0.5f;    

    [Header("Weapon")]
    [SerializeField] private bool _hasMagazine = false;
    [Range(0, 100)]
    [SerializeField] private int _magazineSize = 30;
    [SerializeField] private bool _HasAutoReload = false;

    [Header("Recoil")]
    [SerializeField] private bool _useRecoil = false;
    [Range(0, 50)]
    [SerializeField] private int _recoilForce = 0;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _muzzleParticles;

    private float _nextShotTime;
    private Controller _characterController;
    private Animator _animator;
    private readonly int _weaponUse = Animator.StringToHash(name: "WeaponUse");

    public Character WeaponUser { get; set; }
    public WeaponAmmo WeaponAmmo { get; set; }
    public int CurrentAmmo { get; set; }
    public bool UseMagazine => _hasMagazine;
    public int MagazineSize => _magazineSize;
    public bool CanShoot { get; set; }


    protected virtual void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        _animator = GetComponent<Animator>();
        CurrentAmmo = _magazineSize;
    }

    protected virtual void Update()
    {
        WeaponCanShoot();
        RotateWeapon();
    }

    /// <summary>
    /// Trigger the weapn to shoot
    /// </summary>
    public void WeaponShot()
    {
        HandleShooting();
    }

    /// <summary>
    /// Handles how the weapon shoots
    /// </summary>
    protected virtual void HandleShooting()
    {
        if (UseMagazine)
        {
            if (WeaponAmmo != null)
            {
                if (WeaponAmmo.CanUseWeapon())
                {
                    RequestShot();
                }
                else
                {
                    if (_HasAutoReload)
                    {
                        Reload();
                    }
                }
            }
        }
        else
        {
            RequestShot();
        }
    }

    /// <summary>
    /// Controls the next time to shoot
    /// </summary>
    protected virtual void WeaponCanShoot()
    {
        if (Time.time > _nextShotTime)
        {
            CanShoot = true;
            _nextShotTime = Time.time + _fireRate; // the current time + our time between shots
        }
    }

    /// <summary>
    /// Stop using the weapon
    /// </summary>
    public void StopWeapon()
    {
        if (_useRecoil)
        {
            // stop recoil movement
            _characterController.ApplyRecoil(Vector2.one, 0);
        }
    }

    /// <summary>
    /// Reference to the owner of this weapon
    /// </summary>
    /// <param name="owner">The Character</param>
    public void SetOwner(Character owner)
    {
        WeaponUser = owner;
        _characterController = owner.GetComponent<Controller>();
    }

    public void Reload()
    {
        if (WeaponAmmo != null)
        {
            if (UseMagazine)
            {
                WeaponAmmo.RefillAmmo();
            }
        }
    }

    private void RequestShot()
    {
        if (!CanShoot)
        {
            return;
        }

        if (_useRecoil)
        {
            Recoil();
        }

        _animator.SetTrigger(_weaponUse);
        WeaponAmmo.ConsumeAmmo();
        _muzzleParticles.Play();
    }

    private void Recoil()
    {
        if (_useRecoil)
        {
            // applies movement to our player, applies recoil
            if (WeaponUser != null)
            {
                if (WeaponUser.GetComponent<SpriteFlip>().FacingRight)
                {
                    // add recoil force
                    _characterController.ApplyRecoil(Vector2.left, _recoilForce);
                }
                else
                {
                    _characterController.ApplyRecoil(Vector2.right, _recoilForce);
                }
            }
        }
    }

    protected virtual void RotateWeapon()
    {
        if (WeaponUser.GetComponent<SpriteFlip>().FacingRight)
        {
            transform.localScale = new Vector3(x: 1, y: 1, z: 1);
        }
        else
        {
            transform.localScale = new Vector3(x: -1, y: 1, z: 1);
        }
    }
}