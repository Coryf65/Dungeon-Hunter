using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("settings")]
    [Tooltip("The Time between shots. How long between each shot will take.")]
    [SerializeField] private float _fireRate = 0.5f;

    [Header("Weapon")]
    [SerializeField] private bool _HasMagazine = false;
    [SerializeField] private int _magazineSize = 30;
    [SerializeField] private bool _HasAutoReload = false;

    public Character WeaponUser { get; set; }
    public WeaponAmmo WeaponAmmo { get; set; }
    public int CurrentAmmo { get; set; }
    public bool UseMagazine => _HasMagazine;
    public int MagazineSize => _magazineSize;
    public bool CanShoot { get; set; }

    private float _nextShotTime;

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

    public void SetOwner(Character owner)
    {
        WeaponUser = owner;
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

        WeaponAmmo.ConsumeAmmo();
        CanShoot = false;
    }
}