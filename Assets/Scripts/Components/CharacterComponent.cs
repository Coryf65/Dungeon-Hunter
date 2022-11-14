using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    protected float horizontalInput;
    protected float verticalInput;

    protected Controller controller;
    protected CharacterMovement characterMovement;
    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        controller = GetComponent<Controller>();
        characterMovement = GetComponent<CharacterMovement>();
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAbility();
    }

    /// <summary>
    /// Character Abilities
    /// move, run, dash, shoot
    /// </summary>
    protected virtual void HandleAbility()
    {
        InternalInput();
        HandleInput();
    }

    /// <summary>
    /// Get input to perform actions
    /// get buttons from player
    /// </summary>
    protected virtual void HandleInput()
    {
        
    }

    /// <summary>
    /// Get the input to control our character, things that won't change
    /// x and y movements
    /// </summary>
    protected virtual void InternalInput()
    {
        // GetAxis() applies smoothing
        // GetAxisRaw() is the raw input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}
