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
}