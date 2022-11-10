using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CharacterMovement : CharacterComponent
{
    [SerializeField]
    private float _walkSpeed = 6f;
    private readonly int _movingTrigger = Animator.StringToHash("IsMoving");

    public float MovementSpeed { get; set; }


    protected override void Start()
    {
        base.Start();
        MovementSpeed = _walkSpeed;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        MoveCharacter();
        UpdateAnimations();
    }

    private void MoveCharacter()
    {
        Vector2 movement = new(horizontalInput, verticalInput);
        Vector2 normalizedMovement = movement.normalized;
        Vector2 movementSpeed = movement * MovementSpeed;
        controller.SetMovement(movementSpeed);
    }

    /// <summary>
    /// Reset the movemetn speed back to the original amount
    /// </summary>
    public void ResetMovementSpeed()
    {
        MovementSpeed = _walkSpeed;
    }

    /// <summary>
    /// Handles changing of our aniamtions
    /// </summary>
    private void UpdateAnimations()
    {
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            animator.SetBool(_movingTrigger, true);
        }
        else
        {
            animator.SetBool(_movingTrigger, false);
        }
    }
}