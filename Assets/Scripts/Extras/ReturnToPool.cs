using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private float _lifetime = 2f; // seconds
    [SerializeField] private LayerMask _objectMask;

    private Projectile _projectile;

    private void Start()
    {
        _projectile = GetComponent<Projectile>();
    }

    /// <summary>
    /// Return an object back to the pool
    /// </summary>
    private void Return()
    {
        if (_projectile != null)
        {
            // if we are attached to an Projectile then reset the X scale
            _projectile.ResetProjectile();
        }

        gameObject.SetActive(false);
    }

    /// <summary>
    /// We have collided with something
    /// </summary>
    /// <param name="collidedWith">The GameObject that we have collided with</param>
    private void OnTriggerEnter2D(Collider2D collidedWith)
    {
        if (CheckLayer(collidedWith.gameObject.layer, _objectMask))
        {
            Return();
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
