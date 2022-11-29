using System;
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
    private Collider2D _collider2D;

    // acessor
    public event EventHandler OnJarBroken;    

    void Start()
    {
        _health = GetComponent<Health>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
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

        if (_health.CurrentHealth > 0 && _damagedSprite != null)
        {
            // change sprite
            _spriteRenderer.sprite = _damagedSprite;
        }

        if (_health.CurrentHealth <= 0)
        {
            if (_isDamageable)
            {
                Destroy(gameObject);
            }
            else
            {
                _spriteRenderer.sprite = _damagedSprite;
                // Jar broken
                OnJarBroken?.Invoke(this, EventArgs.Empty);
                _collider2D.enabled = false;
            }
        }
    }
}