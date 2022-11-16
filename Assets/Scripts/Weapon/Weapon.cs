using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("settings")]
    [Tooltip("The Time between shots. How long between each shot will take.")]
    [Range(0.1f, 10f)]
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextShotTime;
    private Controller _characterController;

    [Header("Weapon")]
    [SerializeField] private bool _hasMagazine = false;
    [Range(0, 100)]
    [SerializeField] private int _magazineSize = 30;
    [SerializeField] private bool _HasAutoReload = false;

    [Header("Recoil")]
    [SerializeField] private bool _useRecoil = false;
    [Range(0, 50)]
    [SerializeField] private int _recoilForce = 0;

    public Character WeaponUser { get; set; }
    public WeaponAmmo WeaponAmmo { get; set; }
    public int CurrentAmmo { get; set; }
    public bool UseMagazine => _hasMagazine;
    public int MagazineSize => _magazineSize;
    public bool CanShoot { get; set; }


    private void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        CurrentAmmo = _magazineSize;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
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
    private void HandleShooting()
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

    private void WeaponCanShoot()
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

        WeaponAmmo.ConsumeAmmo();        
        CanShoot = false;
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

    private void RotateWeapon()
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