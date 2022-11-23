using UnityEngine;

public class Potion : Collectables
{
    [Tooltip("How much health this item should give the Player. adds this amount to the current health on pickup.")]
    [Range(0, 20)]
    [SerializeField] private int _healAmount = 1;
    [SerializeField] private ParticleSystem _effects;

    protected override void HandlePickup()
    {
        AddHealth();
    }

    protected override void PlayEffects()
    {
        Instantiate(_effects, transform.position, Quaternion.identity);
    }

    private void AddHealth()
    {
        if (_character != null)
        {
            _character.GetComponent<Health>().GainHealth(_healAmount);
        }
    }
}