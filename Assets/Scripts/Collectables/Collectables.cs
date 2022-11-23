using UnityEngine;

public class Collectables : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("checked: Destroy item on pickup, unchecked: Hides item on pickup")]
    [SerializeField] private bool _canDestroyItem = true;

    protected Character _character;
    protected GameObject _objectCollided;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider2D;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _objectCollided = collision.gameObject;

        // Check if it is a pickup
        if (!IsAPickup(_objectCollided))
            return;

        HandlePickup();
        PlayEffects();
        CleanupObject(_canDestroyItem);
    }

    /// <summary>
    /// Check if the gameobject is a Pickup, looks into GameObject's components for
    /// a Character Component and having the CharacterType == Player.
    /// </summary>
    /// <param name="item">GameObject to check</param>
    /// <returns>If the Item is a Pickup for the player</returns>
    protected virtual bool IsAPickup(GameObject item)
    {
        bool isAPickup = false;
        _character = item.GetComponent<Character>();

        // no character class, cannot use pickup
        if (_character is null)        
            return isAPickup;
        
        if (_character.CharacterType == Character.CharacterTypes.Player)
        {
            isAPickup = true;
        }

        return isAPickup;
    }

    protected virtual void HandlePickup()
    {

    }

    protected virtual void PlayEffects()
    {

    }

    /// <summary>
    /// Handles GameObject Cleanup settings. If the object is set to 
    /// Destroy it will remove. otherwise after pickup it hides the object.
    /// </summary>
    private void CleanupObject(bool destroyItem)
    {
        if (destroyItem)
        {
            Destroy(gameObject);
        }

        if (!destroyItem)
        {
            _spriteRenderer.enabled = false;
            _collider2D.enabled = false;
        }
    }
}