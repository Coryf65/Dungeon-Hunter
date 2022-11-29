using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item")]
public class ShopItems : ScriptableObject
{
    public string ItemName;
    public Collectables ItemCollectable;
    public Weapon WeaponToSell;
    public int Cost;
}
