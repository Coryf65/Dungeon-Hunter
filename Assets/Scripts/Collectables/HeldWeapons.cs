using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CWeapon
public class Chara : Collectables
{
    [SerializeField] private ItemData weaponData;

    protected override void HandlePickup()
    {
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (_character != null)
        {
            _character.GetComponent<CharacterWeapon>().SecondaryWeapon = weaponData.weapon;
        }
    }
}