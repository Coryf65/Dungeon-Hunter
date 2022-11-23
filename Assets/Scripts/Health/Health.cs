using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float _startingHealth = 1f;
    [SerializeField] private float _maxHealth = 1f;

    [Header("Shield")]
    [SerializeField] private float _startingShield = 0f;
    [SerializeField] private float _maxShield = 0f;

    [Header("info")]
    [SerializeField] private bool _isDestroyed = false;
    [SerializeField] private bool _isShieldDestroyed = false;
    [SerializeField] private bool _isPlayer = false;

    private Character _character;
    private Controller _characterController;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    /// <summary>
    /// The current health of the object
    /// </summary>
    public float CurrentHealth { get; set; }
    public float CurrentShield { get; set; }

    /// <summary>
    /// Runs on first startup
    /// </summary>
    private void Awake()
    {
        _character = GetComponent<Character>();
        _characterController = GetComponent<Controller>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        
        CurrentHealth = _startingHealth;
        CurrentShield = _startingShield;

        // only update the UI if it has a character component
        if (_character != null)
        {
            _isPlayer = _character.CharacterType == Character.CharacterTypes.Player;
            UIManager.Instance.UpdateHealth(CurrentHealth, _maxHealth, CurrentShield, _maxShield, _isPlayer);
        }        
    }

    /// <summary>
    /// Runs every frame call
    /// </summary>
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
        // check if we have any health
        if (CurrentHealth <= 0)
        {
            return;
        }

        // shield logic
        if (!_isShieldDestroyed && _character is not null)
        {
            // only our player can have a shield if a box is the GameObject it wont pass this
            CurrentShield -= damage;

            if (CurrentShield <= 0)
            {
                _isShieldDestroyed = true;
            }
            
            // keep breaking the shield            
            UIManager.Instance.UpdateHealth(CurrentHealth, _maxHealth, CurrentShield, _maxShield, _isPlayer);
            return;
        }

        CurrentHealth -= damage;
        UIManager.Instance.UpdateHealth(CurrentHealth, _maxHealth, CurrentShield, _maxShield, _isPlayer);

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
            _characterController.enabled = false;
            _isDestroyed = true;
        }

        if (_isDestroyed)
        {
            CleanupObject();
        }
    }

    /// <summary>
    /// Revive this object
    /// </summary>
    public void Revive()
    {
        if (_character is not null)
        {
            _isDestroyed = false;
            CurrentHealth = _startingHealth;
            _isShieldDestroyed = false;
            CurrentShield = _startingShield;

            _collider.enabled = true;
            _spriteRenderer.enabled = true;
            _characterController.enabled = true;
        }

        gameObject.SetActive(true);
        UIManager.Instance.UpdateHealth(CurrentHealth, _maxHealth, CurrentShield, _maxShield, _isPlayer);
    }

    /// <summary>
    /// Cleanup of any items on destroy / removal of this object
    /// </summary>
    private void CleanupObject()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Gives the Player the amount of health passed in addtion to it's Current Health
    /// </summary>
    /// <param name="amount"></param>
    public void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, _maxHealth);
        UIManager.Instance.UpdateHealth(CurrentHealth, _maxHealth, CurrentShield, _maxShield, _isPlayer);
    }
}