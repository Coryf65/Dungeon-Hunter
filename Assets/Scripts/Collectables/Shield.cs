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

    protected override void PlayEffects()
    {
        Instantiate(_effect, transform.position, Quaternion.identity);
    }

    private void AddShield()
    {
        _character.GetComponent<Health>().GainShield(_shieldAmount);
    }
}