using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon selectedWeapon;
    [SerializeField] private Transform weaponHolderPosition;

    public Weapon CurrentWeapon { get; set; }
    public WeaponAim WeaponAim { get; set; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(selectedWeapon, weaponHolderPosition);
    }

    protected override void HandleInput()
    {
        // shoot, left click
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopWeapon();
        }

        // reload, R
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.WeaponShot();
    }

    public void Reload()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Reload();
    }

    public void StopWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.StopWeapon();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="weapon"></param>
    /// <param name="position"></param>
    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition; // setting it as a child
        CurrentWeapon.SetOwner(character);
        WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();
    }
}