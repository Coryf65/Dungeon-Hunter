using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectables
{
    [SerializeField] private int _value = 1;

    protected override void HandlePickup()
    {
        AddCoins();
    }

    private void AddCoins()
    {
        CoinManager.Instance.AddCoins(_value);
    }
}