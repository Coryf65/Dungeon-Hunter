using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Movement : Abilities
{
    [SerializeField]
    private float _walkSpeed = 6f;

    public float WalkMovementSpeed { get; set; }

    protected override void Start()
    {
        base.Start();
        WalkMovementSpeed = _walkSpeed;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
    }

    private void MoveCharacter()
    {
        Vector2 movement = new(horizontalInput, verticalInput);
        Vector2 normalizedMovement = movement.normalized;
        Vector2 movementSpeed = movement * _walkSpeed;
        controller.SetMovement(movementSpeed);
    }
}