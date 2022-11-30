using System;
using UnityEngine;

public class Potion : Collectables
{
    [Tooltip("How much health this item should give the Player. adds this amount to the current health on pickup.")]
    [Range(0, 20)]
    [SerializeField] private int _healAmount = 1;
    [Header("Pickup Effect")]
    [SerializeField] private ParticleSystem _effects;

    protected override void HandlePickup()
    {
        AddHealth();
    }
    
    /// <summary>
    /// Play effects to the pickup
    /// </summary>
    protected override void PlayEffects()
    {
        Instantiate(_effects, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Add health to the attached player
    /// </summary>
    public void AddHealth()
    {
        if (_character != null)
        {
            _character.GetComponent<Health>().GainHealth(_healAmount);
        }
    }

    /// <summary>
    /// add health to the player character passed in
    /// </summary>
    /// <param name="character">the player character to give health</param>
    public void AddHealth(Character character)
    {
        if (character != null)
        {
            character.GetComponent<Health>().GainHealth(_healAmount);
        }
    }
}