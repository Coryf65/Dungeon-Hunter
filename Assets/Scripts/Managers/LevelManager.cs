using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls where we spawn our characters
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character _character;    
    [SerializeField] private Transform _spawnPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key: KeyCode.P))
        {
            ReviveCharacter();
        }
    }

    private void ReviveCharacter()
    {
        if (_character.GetComponent<Health>().CurrentHealth <= 0)
        {
            _character.GetComponent<Health>().Revive();
            _character.transform.position = _spawnPosition.position;
        }
    }
}
