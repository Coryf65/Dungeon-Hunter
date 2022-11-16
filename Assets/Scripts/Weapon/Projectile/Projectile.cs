using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _acceleration = 0f;
    [SerializeField] private float _adjustment = 10f;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _movement;

    public Vector2 Direction { get; set; }
    public bool IsFacingRight { get; set; }
    public float Speed { get; set; }

    private void Awake()
    {
        Speed = _speed;
        IsFacingRight = true;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
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

    public void ResetProjectile()
    {
        _spriteRenderer.flipX = false;
    }
}