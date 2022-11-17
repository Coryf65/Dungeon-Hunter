using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBase : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] private Sprite _damagedSprite;

    [Header("Settings")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private bool _isDamageable = true;

    private Health _health;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        _health.TakeDamage(_damage);

        if (_health.CurrentHealth > 0 && _damagedSprite != null && _isDamageable)
        {
            // change sprite
            _spriteRenderer.sprite = _damagedSprite;
        }

        if (_health.CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
