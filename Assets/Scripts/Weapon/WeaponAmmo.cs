using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private Weapon _weapon;
    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        RefillAmmo();
    }

    /// <summary>
    /// decrements ammo by one
    /// </summary>
    public void ConsumeAmmo()
    {
        // if we use a magazine then consume
        if (_weapon.UseMagazine)
        {
            _weapon.CurrentAmmo -= 1;
        }
    }

    /// <summary>
    /// Reloads Ammo
    /// </summary>
    public void RefillAmmo()
    {
        if (_weapon.UseMagazine)
        {
            _weapon.CurrentAmmo = LoadAmmo();
        }
    }

    /// <summary>
    /// Check to see if we have enough ammo to fire
    /// </summary>
    /// <returns>T / F if we can fire our weapon</returns>
    public bool CanUseWeapon()
    {
        if (_weapon.CurrentAmmo > 0)
        {
            return true;
        }

        return false;
    }

    public void SaveAmmo()
    {
        PlayerPrefs.SetInt(key: WEAPON_AMMO_SAVELOAD + _weapon.WeaponName, value: _weapon.CurrentAmmo);
    }

    public int LoadAmmo()
    {
        return PlayerPrefs.GetInt(key: WEAPON_AMMO_SAVELOAD + _weapon.WeaponName, defaultValue: _weapon.MagazineSize);
    }
}