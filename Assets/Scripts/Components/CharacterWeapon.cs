using System;
using UnityEngine;

public class CharacterWeapon : CharacterComponent
{
    [Header("Weapon Settings")]
    [SerializeField] private Weapon _selectedWeapon;
    [SerializeField] private Transform _weaponHolderPosition;
    public static Action OnStartShooting;
    public Weapon CurrentWeapon { get; set; }
    public Weapon SecondaryWeapon { get; set; }
    public WeaponAim WeaponAim { get; set; }

    /// <summary>
    /// Setup Player Weapon
    /// </summary>
    protected override void Start()
    {
        base.Start();
        EquipWeapon(_selectedWeapon, _weaponHolderPosition);
    }

    /// <summary>
    /// Handle Player Input
    /// </summary>
    protected override void HandleInput()
    {
        // shoot, left click
        if (Input.GetMouseButton(0)) //Input.GetMouseButtonDown(0) || 
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

        // Primary Weapon, 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && SecondaryWeapon != null)
        {
            EquipWeapon(_selectedWeapon, _weaponHolderPosition);
        }

        // Secondary Weapon, 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && SecondaryWeapon != null)
        {
            EquipWeapon(SecondaryWeapon, _weaponHolderPosition);
        }
    }

    /// <summary>
    /// Shoot our Weapon
    /// </summary>
    public void Shoot()
    {
        if (CurrentWeapon == null)
            return;

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
            return;

        CurrentWeapon.Reload();

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }

    public void StopWeapon()
    {
        if (CurrentWeapon == null) 
            return;

        CurrentWeapon.StopWeapon();
    }

    /// <summary>
    /// Equip the selected weapon
    /// </summary>
    /// <param name="weapon">Weapon to use</param>
    /// <param name="weaponPosition">Where to Spawn our weapon</param>
    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        if (CurrentWeapon != null)
        {
            WeaponAim.DestroyReticle();
            Destroy(GameObject.Find("Pooled Objects"));
            Destroy(CurrentWeapon.gameObject);
        }

        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition; // setting it as a child
        CurrentWeapon.SetOwner(character);
        WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();

        // only update if it's a player
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
            UIManager.Instance.UpdateWeaponSprite(CurrentWeapon.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        }
    }
}