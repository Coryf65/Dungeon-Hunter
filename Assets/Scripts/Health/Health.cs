using UnityEngine;

public class Health : MonoBehaviour
{
    private Character _character;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    [Header("Health")]
    [SerializeField] private float _startingHealth = 10f;
    [SerializeField] private float _maxHealth = 10f;

    [Header("Settings")]
    [SerializeField] private bool _IsDestroyed = false;

    /// <summary>
    /// The current health of the object
    /// </summary>
    public float CurrentHealth { get; set; }

    /// <summary>
    /// Runs on first startup
    /// </summary>
    private void Awake()
    {
        _character = GetComponent<Character>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        CurrentHealth = _startingHealth;
    }

    private void Update()
    {
        // testing
        if (Input.GetKeyDown(key: KeyCode.L))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(key: KeyCode.P))
        {
            Revive();
        }
    }

    /// <summary>
    /// Gives the object x amount of damage.
    /// </summary>
    /// <param name="damage">amount of damage done</param>
    public void TakeDamage(int damage)
    {
        Debug.Log("current health = " + CurrentHealth);

        // check if we have any health
        if (CurrentHealth <= 0)
        {
            return;
        }

        CurrentHealth -= damage;

        // no health left after damage
        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// Destroys the object
    /// </summary>
    public void Death()
    {
        if (_character is not null)
        {
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
            _IsDestroyed = true;
        }

        if (_IsDestroyed)
        {
            CleanupObject();
        }
        Debug.Log("current health = " + CurrentHealth);
    }

    /// <summary>
    /// Revive this object
    /// </summary>
    public void Revive()
    {        
        if (_character is not null)
        {
            _IsDestroyed = false;
            CurrentHealth = _startingHealth;
            _collider.enabled = true;
            _spriteRenderer.enabled = true;
        }
        
        gameObject.SetActive(true);
        Debug.Log("current health = " + CurrentHealth);
    }

    /// <summary>
    /// Cleanup of any items on destroy / removal of this object
    /// </summary>
    private void CleanupObject()
    {
        gameObject.SetActive(false);
    }
}
