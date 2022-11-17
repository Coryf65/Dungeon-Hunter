using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _acceleration = 0f;
    [SerializeField] private float _adjustment = 10f;
    [Header("Collision Layer")]
    [SerializeField] private LayerMask _objectMask;
    [Header("Effects")]
    [SerializeField] private ParticleSystem _impactEffect;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private Vector2 _movement;
    private bool _canMove = true;

    public Vector2 Direction { get; set; }
    public bool IsFacingRight { get; set; }
    public float Speed { get; set; }

    private void Awake()
    {
        Speed = _speed;
        IsFacingRight = true;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(_canMove)
            MoveProjectile();
    }

    public void MoveProjectile()
    {
        // testing
        _movement = Direction * (Speed / _adjustment) * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + _movement);

        Speed += _acceleration * Time.deltaTime;
    }

    /// <summary>
    /// Flips this projectile
    /// </summary>
    public void FlipProjectile()
    {
        if (_spriteRenderer != null)
        {
            // flip to the opposite to what it was
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    /// <summary>
    /// Set the direction and rotation in order to move
    /// </summary>
    /// <param name="direction">New direction</param>
    /// <param name="rotation">Users rotation</param>
    /// <param name="isFacingRight">Users Flip direction value</param>
    public void SetDirection(Vector2 direction, Quaternion rotation, bool isFacingRight = true)
    {
        Direction = direction;

        if (IsFacingRight != isFacingRight)
        {
            FlipProjectile();
        }

        transform.rotation = rotation;
    }

    /// <summary>
    /// Resets the projectiles components ready to be pooled again.
    /// </summary>
    public void ResetProjectile()
    {
        _spriteRenderer.flipX = false;
        _canMove = true;
        _spriteRenderer.enabled = true;
        _collider2D.enabled = true;
    }

    /// <summary>
    /// We have collided with something
    /// </summary>
    /// <param name="collidedWith">The GameObject that we have collided with</param>
    private void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (CheckLayer(collidedWith.gameObject.layer, _objectMask))
        {
            _canMove = false;
            _spriteRenderer.enabled = false;
            _collider2D.enabled = false;
            _impactEffect.Play();
        }
    }

    /// <summary>
    /// Compare the layer to another layer (objectMask)
    /// </summary>
    /// <param name="layer">Layer to compare with</param>
    /// <param name="objectMask">Your layer that you are checking for</param>
    /// <returns>t/f if the layers are the same</returns>
    private bool CheckLayer(int layer, LayerMask objectMask)
    {
        // we have a comparison that returns true
        return ((1 << layer) & objectMask) != 0;
    }
}