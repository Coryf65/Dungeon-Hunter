using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterTypes
    {
        Player,
        AI
    }

    [SerializeField] private CharacterTypes _characterType;
    [SerializeField] private GameObject _characterSprite;
    [SerializeField] private Animator _characterAnimator;

    public CharacterTypes CharacterType => _characterType;
    public GameObject CharacterSprite => _characterSprite;
    public Animator Animator => _characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
