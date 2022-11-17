using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBase : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    private Health _health;

    void Start()
    {
        _health = GetComponent<Health>();
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

        if (_health.CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
