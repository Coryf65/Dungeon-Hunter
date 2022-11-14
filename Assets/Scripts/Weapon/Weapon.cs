using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("settings")]
    [Tooltip("The Time between shots. How long between each shot will take.")]
    [SerializeField] private float fireRate = 0.5f;

    [Header("Weapon")]
    [SerializeField] private bool _HasMagazine = false;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool _HasAutoReload = false;

    public Character WeaponUser { get; set; }
    public int CurrentAmmo { get; set; }
    public bool UseMagazine => _HasMagazine;

    private void Awake()
    {
        CurrentAmmo = magazineSize;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void WeaponShot()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {

    }

    public void SetOwner(Character owner)
    {
        WeaponUser = owner;
    }

    public void Reload()
    {

    }
}