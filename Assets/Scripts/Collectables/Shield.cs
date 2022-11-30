using System;
using UnityEngine;

public class Shield : Collectables
{
    [Tooltip("The amount of shield to add to the current player's Health")]
    [SerializeField] private int _shieldAmount = 1;
    [Header("Pickup Effect")]
    [SerializeField] private ParticleSystem _effect;

    protected override void HandlePickup()
    {
        AddShield();
    }

    /// <summary>
    /// Play FX
    /// </summary>
    protected override void PlayEffects()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Adds Shield to the player
    /// </summary>
    public void AddShield()
    {
        _character.GetComponent<Health>().GainShield(_shieldAmount);
    }

    internal void AddShield(Character character)
    {
        character.GetComponent<Health>().GainShield(_shieldAmount);
    }
}