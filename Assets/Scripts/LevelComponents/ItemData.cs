using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon", fileName = "Item Weapon")]
public class ItemData : ScriptableObject
{
    [Header("Weapon to Equip")]
    public Weapon weapon;
    [Header("Weapons Sprite")]
    public Sprite sprite;

}
