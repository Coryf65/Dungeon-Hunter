using System;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon selectedWeapon;
    [SerializeField] private Transform weaponHolderPosition;
    public static Action OnStartShooting;
    public Weapon CurrentWeapon { get; set; }
    public WeaponAim WeaponAim { get; set; }

    /// <summary>
    /// Setup Player Weapon
    /// </summary>
    protected override void Start()
    {
        base.Start();
        EquipWeapon(selectedWeapon, weaponHolderPosition);
    }

    /// <summary>
    /// Handle Player Input
    /// </summary>
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

    /// <summary>
    /// Shoot our Weapon
    /// </summary>
    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.WeaponShot();

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            // we only want to do this for players
            OnStartShooting?.Invoke();
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    /// <summary>
    /// Reload our weapon
    /// </summary>
    public void Reload()
    {
        if (CurrentWeapon == null)
        {
            return;
        }

        CurrentWeapon.Reload();

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
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
    /// Equip the selected weapon
    /// </summary>
    /// <param name="weapon">Weapon to use</param>
    /// <param name="position">Where to Spawn our weapon</param>
    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition; // setting it as a child
        CurrentWeapon.SetOwner(character);
        WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();

        // only update if it's a player
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }
}