using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Tooltip("Prefab to create a pool for, ie: a bullet for a weapon")]
    [SerializeField] private GameObject _objectPrefab;
    [Range(0, 100)]
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private bool _poolCanExpand = true;

    private List<GameObject> _pooledObjects;
    private GameObject _parentObject;

    // Start is called before the first frame update
    private void Start()
    {
        _parentObject = new(name: "Pooled Objects");
        Refill();
    }

    /// <summary>
    /// Create our pool
    /// </summary>
    public void Refill()
    {
        _pooledObjects = new();

        for (int i = 0; i < _poolSize; i++)
        {
            AddObjectToPool();
        }
    }

    /// <summary>
    /// return one object from our pool
    /// </summary>
    /// <returns></returns>
    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            // find a disabled object to use
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        if (_poolCanExpand)
        {
            // none left to use we need to expand
            return AddObjectToPool();
        }

        return null;
    }

    /// <summary>
    /// Adds one object into the existing pool
    /// </summary>
    /// <returns></returns>
    public GameObject AddObjectToPool()
    {
        GameObject gameObject = Instantiate(_objectPrefab);
        gameObject.SetActive(false);
        gameObject.transform.SetParent(_parentObject.transform, true);
        _pooledObjects.Add(gameObject);

        return gameObject;
    }
}