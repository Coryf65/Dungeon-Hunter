using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [SerializeField] private float _lifetime = 2f; // seconds

    /// <summary>
    /// Return an object back to the pool
    /// </summary>
    private void Return()
    {
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
