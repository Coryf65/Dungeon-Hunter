using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private Weapon _weapon;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
    }

    public void ConsumeAmmo()
    {
        // if we use a magazine then consume
        if (_weapon.UseMagazine)
        {
            _weapon.CurrentAmmo -= 1;
        }
    }

    public void RefillAmmo()
    {
        if (_weapon.UseMagazine)
        {
            _weapon.CurrentAmmo = 0;
        }
    }

    public bool CanUseWeapon()
    {
        if (_weapon.CurrentAmmo > 0)
        {
            return true;
        }

        return false;
    }
}