using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Pooling Settings")]
    [SerializeField] private float _lifetime = 2f; // seconds    

    private Projectile _projectile;

    private void Start()
    {
        _projectile = GetComponent<Projectile>();
    }

    /// <summary>
    /// Return an object back to the pool
    /// </summary>
    public void Return()
    {
        if (_projectile != null)
        {
            // if we are attached to an Projectile then reset the X scale
            _projectile.ResetProjectile();
        }

        gameObject.SetActive(false);
    }    

    /// <summary>
    /// When this game object gets enabled / set active
    /// </summary>
    private void OnEnable()
    {
        Invoke(nameof(Return), _lifetime);
    }

    /// <summary>
    /// When this game object gets Dis-abled / set Inactive
    /// </summary>
    private void OnDisable()
    {
        CancelInvoke();
    }
}
